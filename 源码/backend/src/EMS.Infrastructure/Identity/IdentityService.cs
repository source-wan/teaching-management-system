using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EMS.App.Common.Config;
using EMS.App.Common.Interface;
using EMS.App.ReqDto;
using EMS.App.ResDto;
using EMS.App.Util;
using EMS.Common.Interface;
using EMS.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EMS.Infrastructure.Identity
{
    public class IdentityService : IIdentity
    {
        private readonly IConfiguration _config;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IRepository<StudentInfo> _studentInfo;
        private readonly IRepository<TeacherInfo> _teacherInfo;
        private readonly IRepository<IdentityUser> _identityUser;
        private readonly IRepository<UserGroupInfo> _userGroupInfo;
        private readonly IRepository<UserGroupUserInfo> _userGroupUserInfo;
        private readonly IRepository<RoleInfo> _roleInfo;
        private readonly IUpdateDataServer<UserInfo, LoginUserDto> _updateUserServer;

        public IdentityService
        (
            IConfiguration config,
            IRepository<UserInfo> userInfo,
            IRepository<StudentInfo> studentInfo,
            IRepository<TeacherInfo> teacherInfo,
            IRepository<UserGroupInfo> userGroupInfo,
            IRepository<UserGroupUserInfo> userGroupUserInfo,
            IRepository<RoleInfo> roleInfo,
            IRepository<IdentityUser> identityUser,
            IUpdateDataServer<UserInfo, LoginUserDto> updateUserServer
        )
        {
            _config = config;
            _userInfo = userInfo;
            _studentInfo = studentInfo;
            _teacherInfo = teacherInfo;
            _userGroupInfo = userGroupInfo;
            _userGroupUserInfo = userGroupUserInfo;
            _roleInfo = roleInfo;
            _identityUser = identityUser;
            _updateUserServer = updateUserServer;
        }

        public async Task<TokenDto> GenerateToken(UserInfo userInfo)
        {
            var jwtSetting = _config.GetSection("JwtSetting").Get<JwtSetting>();

            var roleName = GetRoleName(userInfo, out var roleGuid).ToList();
            var claims = new List<Claim>
            {
                new Claim("UserId",userInfo!.Id.ToString()),
                new Claim("Account",userInfo!.Account),
                new Claim("WeChatOpenId", userInfo.WeChatOpenId ?? "")
            };

            foreach (var roleTemp in roleName)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleTemp.ToString().ToLower()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(jwtSetting.Issuer, jwtSetting.Audience, claims, expires: DateTime.UtcNow.AddMinutes(jwtSetting.AccessExpiration), signingCredentials: credentials);

            //生成token
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            var refreshToken = GenerateRefreshToken();

            var tmpIdentityUser = new IdentityUser
            {
                Account = userInfo.Account,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(jwtSetting.RefreshExpiration)
            };

            var IdentityUser = _identityUser.Table.Where(x => x.Id == userInfo.Id).ToList();
            await _identityUser.DeleteAsync(IdentityUser, true);
            await _identityUser.AddAsync(tmpIdentityUser);

            return new TokenDto
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenDto> GenerateRefreshToken(TokenDto token)
        {
            var jwtSetting = _config.GetSection("JwtSetting").Get<JwtSetting>();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting.Issuer,
                ValidAudience = jwtSetting.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken)
            {
                throw new SecurityTokenException("token无效");
            }

            if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("错误的加密算法");
            }

            var userId = new Guid(principal.FindFirstValue("UserId"));
            var user =
                _identityUser.Table.FirstOrDefault(identityUser => identityUser.Id == userId);

            if (user == null || user.RefreshToken != token.RefreshToken)
            {
                throw new Exception("用户不存在!");
            }

            if (user.RefreshTokenExpiration <= DateTime.Now)
            {
                throw new Exception("Refresh token 已经过期了");
            }

            var appUser = _userInfo.Table.FirstOrDefault(user => user.Id == userId);
            if (appUser != null)
            {
                var roleName = principal.FindFirstValue("RoleName");
                if (!this.GetRoleName(appUser).Any(role => role.ToString() == roleName))
                {
                    throw new Exception("角色验证失败");
                }

                return await GenerateToken(appUser);
            }
            else
            {
                return new TokenDto();
            }
        }

        public async Task<TokenDto> ValidateUserAsync(LoginUserDto userForAuth)
        {
            if (!userForAuth.IsNullOrEmpty())
            {
                UserInfo? appUser = null;

                // 没携带 WeChatOpenID
                if (userForAuth.WeChatOpenId.IsNullOrEmpty())
                {
                    appUser = _userInfo.Table.FirstOrDefault
                    (
                        user =>
                            (user.Account == userForAuth.Account
                            && user.Password == userForAuth.Password)
                    );
                }
                else
                {
                    appUser = _userInfo.Table.FirstOrDefault(user => user.WeChatOpenId == userForAuth.WeChatOpenId);
                }

                if (appUser != null) return await this.GenerateToken(appUser);
            }

            return new TokenDto();
        }

        private string GenerateRefreshToken()
        {
            var rndNumber = new byte[32];

            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(rndNumber);

            return Convert.ToBase64String(rndNumber);
        }

        private IEnumerable<RoleType> GetRoleName(UserInfo userInfo, out IEnumerable<Guid> roleInfoGuid)
        {
            var roleInfo = GetRoleInfo(userInfo);
            roleInfoGuid = roleInfo.Select(role => role.Id);
            return roleInfo.Select(role => role.RoleName);
        }

        private IEnumerable<RoleType> GetRoleName(UserInfo userInfo)
        {
            return GetRoleInfo(userInfo).Select(role => role.RoleName);
        }
        private IEnumerable<RoleInfo> GetRoleInfo(UserInfo userInfo)
        {
            return from user in _userInfo.Table
                   where user.Account == userInfo.Account
                   && user.Password == userInfo.Password
                   join userGroupUser in _userGroupUserInfo.Table
                   on user.Id equals userGroupUser.UserId into userGroupInfoTable
                   from userGroupInfo in userGroupInfoTable.DefaultIfEmpty()
                   join groupInfo in _userGroupInfo.Table
                   on userGroupInfo.UserGroupId equals groupInfo.Id into groupInfoTable
                   from groupInfo in groupInfoTable.DefaultIfEmpty()
                   join role in _roleInfo.Table
                   on groupInfo.RoleId equals role.Id into roleInfoTable
                   from role in roleInfoTable.DefaultIfEmpty()
                   select role;
        }

        public UserGroupInfo? GetUserGroupInfo(RoleType roleType)
        {
            return (from role in _roleInfo.Table
                    where role.RoleName == roleType
                    join groupInfo in _userGroupInfo.Table
                    on role.Id equals groupInfo.RoleId
                    select groupInfo).FirstOrDefault();
        }
    }
}