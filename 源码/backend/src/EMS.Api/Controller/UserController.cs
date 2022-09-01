namespace EMS.Api.Controller;
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository<UserInfo> _appUser;
    private readonly IGetDataServer<StudentInfo> _getStudentInfoServer;
    private readonly IIdentity _identity;

    public UserController(IRepository<UserInfo> appUser,
    IGetDataServer<StudentInfo> getStudentInfoServer,
    IIdentity identity)
    {
        _appUser = appUser;
        _getStudentInfoServer = getStudentInfoServer;
        _identity = identity;
    }

    [HttpPost("/login")]
    public async Task<string> LoginHandleAsync([FromBody] LoginUserDto reqUser)
    {
        var token = await _identity.ValidateUserAsync(reqUser);

        if (token.AccessToken == null || token.RefreshToken == null)
        {
            return new
            {
                Code = 4000,
                Msg = "账号或密码错误,请检查后重新输入!",
                Data = token
            }.SerializeObject();
        }

        return new
        {
            Code = 1000,
            Msg = "登入成功",
            Data = token
        }.SerializeObject();
    }

    [HttpPost("/refresh")]
    public async Task<string> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var token = await _identity.GenerateRefreshToken(tokenDto);
        if (token != null)
        {
            var res = new
            {
                Code = 1000,
                Data = token,
                Msg = "刷新成功"
            };
            return res.SerializeObject();
        }
        else
        {
            var res = new
            {
                Code = 4000,
                Data = token,
                Msg = "请确认重试"
            };
            return res.SerializeObject();
        }

    }

    [HttpPost("/bind")]
    public async Task<string> BindStudentWeChat([FromBody] BindWeChatDto weChatDto)
    {
        var studentInfo = _getStudentInfoServer.GetDataList(student => weChatDto.StudentCode.Equals(student.StudentCode), out int count).FirstOrDefault();
        if (studentInfo == null)
        {
            return new ResponseDto
            {
                Code = ResponseCode.NotFound,
                Message = $"学号为{weChatDto.StudentCode}的学生不存在",
                Data = null
            }.SerializeObject();
        }

        var user = _appUser.Table.FirstOrDefault(user => user.Id.Equals(studentInfo.UserId) && !user.IsDeleted && user.IsActived);
        {
            if (user == null)
            {
                return new ResponseDto
                {
                    Code = ResponseCode.Fail,
                    Message = $"学号为{weChatDto.StudentCode}的学生账号已被注销，或者尚未注册"
                }.SerializeObject();
            }

            user.WeChatOpenId = weChatDto.WeChatOpenId;
            var origin = await _appUser.UpdateAsync(user);
            var loginUser = new LoginUserDto
            {
                Account = user.Account,
                Password = user.Password,
                WeChatOpenId = user.WeChatOpenId
            };
            var token = await _identity.ValidateUserAsync(loginUser);
            return new
            {
                Code = 1000,
                Msg = "登入成功",
                Data = token,
                Info=new ResponseDto().UpdateDataSuccess(origin, user)
            }.SerializeObject();
        }
    }

    /// <summary>
    /// 已经废弃， 登入用的账号由系统自己生成，无需注册
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin,teacher,instroctor")]
    [HttpPost("/reg")]
    public async Task<string> Reg([FromBody] RegUserDto userDto)
    {
        var appUser = new UserInfo
        {
            Account = userDto.Account,
            Password = userDto.Password
        };

        if (_appUser.Table.FirstOrDefault(x => x.Account == appUser.Account) != null)
        {
            return new
            {
                Code = 4000,
                Data = appUser,
                Msg = $"用户名: {appUser.Account} 已被注册"
            }.SerializeObject();
        }

        if (appUser.Account.Length > 16 || appUser.Password.Length < 8)
        {
            return new
            {
                Code = 4000,
                Data = appUser,
                Msg = $"无效的用户名或密码: 用户名:{appUser.Account}的长度大于16 或(及)密码:{appUser.Password}的长度小于8",
            }.SerializeObject();
        }

        await _appUser.AddAsync(appUser);
        var res = new
        {
            Code = 1000,
            Data = appUser,
            Msg = "注册成功"
        };
        return res.SerializeObject();
    }
}
