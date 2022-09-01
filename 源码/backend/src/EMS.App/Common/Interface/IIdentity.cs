using EMS.App.ReqDto;
using EMS.App.ResDto;
using EMS.Domain.Entity;

namespace EMS.Common.Interface
{
    public interface IIdentity
    {
        Task<TokenDto> ValidateUserAsync(LoginUserDto userForAuth);
        Task<TokenDto> GenerateToken(UserInfo user);
        Task<TokenDto> GenerateRefreshToken(TokenDto token);
        UserGroupInfo? GetUserGroupInfo(RoleType roleType);
    }
}