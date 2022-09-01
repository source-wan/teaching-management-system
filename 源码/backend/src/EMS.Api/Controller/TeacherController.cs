namespace EMS.Api.Controller;
[Route("[controller]")]
[Authorize(Roles = "admin,teacher,instructor")]
public class TeacherController : ControllerBase
{
    private readonly IRepository<UserInfo> _userInfo;
    private readonly IRepository<TeacherInfo> _TeacherInfo;
    private readonly IRepository<RoleInfo> _roleInfo;
    private readonly IRepository<UserGroupInfo> _userGroupInfo;
    private readonly IRepository<UserGroupUserInfo> _userGroupUserInfo;
    private readonly IIdentity _identity;
    private readonly IQueryHelper<TeacherInfo> _queryHelper;
    private readonly IUpdateDataServer<TeacherInfo, UpdateTeacherDto> _updateServer;

    public TeacherController
    (
        IRepository<UserInfo> userInfo,
        IRepository<RoleInfo> roleInfo,
        IRepository<UserGroupInfo> userGroupInfo,
        IRepository<UserGroupUserInfo> userGroupUserInfo,
        IRepository<TeacherInfo> appStudent,
        IIdentity identity,
        IQueryHelper<TeacherInfo> queryHelper,
        IUpdateDataServer<TeacherInfo, UpdateTeacherDto> updateServer
    )
    {
        _userInfo = userInfo;
        _roleInfo = roleInfo;
        _userGroupInfo = userGroupInfo;
        _userGroupUserInfo = userGroupUserInfo;
        _TeacherInfo = appStudent;
        _identity = identity;
        _queryHelper = queryHelper;
        _updateServer = updateServer;
    }

    [HttpGet("/teacher/{teacherId}")]
    # region 获取教师详细信息
    public async Task<string> GetTeacherInfo(Guid teacherId)
    {
        var student = await _TeacherInfo.GetById(teacherId);
        if (student == null) return ReturnStudentIsEmpty(teacherId);

        return new
        {
            Code = 1000,
            Data = student,
            Msg = "获取教师信息成功"
        }.SerializeObject();
    }
    #endregion

    [HttpGet("/teacher")]
    # region 获取教师列表
    public string GetStudentList()
    {
        int index = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
        int size = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");

        var Teachers = _TeacherInfo.Table.Where(_queryHelper.ValidateData);
        var count = Teachers.Count();

        if (count > 0)
        {
            if (Request.Query.ContainsKey("orderbydesc") && (!Request.Query.ContainsKey("orderby")))
            {
                Teachers = Teachers
                .OrderByDescending(_queryHelper.SortByRequestParam)
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            }
            else
            {
                Teachers = Teachers
                .OrderBy(_queryHelper.SortByRequestParam)
                .Where(_queryHelper.ValidateData)
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            }
        }

        return new
        {
            Code = 1000,
            Data = new
            {
                Data = Teachers,
                Count = count
            },
            Msg = "获取教师花名册成功"
        }.SerializeObject();
    }
    #endregion





    [HttpPost("/teacher")]
    # region 添加教师信息,并注册账号
    public async Task<string> CreateTeacherInfoAsync([FromBody] CreateTeacherDto teacherDto)
    {
        var group = _identity.GetUserGroupInfo(RoleType.Teacher);
        var group2 = _identity.GetUserGroupInfo(RoleType.Instructor);
        // 检查学生组是否已经被创建,如果没有则响应相关信息
        if (group2 == null)
        {
            return new
            {
                Code = "4000",
                Msg = "辅导员组尚未被创建,请创建后重试"
            }.SerializeObject();
        }
        if (group == null)
        {
            return new
            {
                Code = "4000",
                Msg = "教师组尚未被创建,请创建后重试"
            }.SerializeObject();
        }
        // 必须的信息是否为空,如果为空则响应相关信息
        if (teacherDto.Name.IsNullOrEmpty() || teacherDto.IdentityCode.IsNullOrEmpty())
        {
            return new
            {
                Code = 3003,
                Msg = @$"教师姓名:{teacherDto.Name}或(及)身份证号:{teacherDto.IdentityCode} 为空"
            }.SerializeObject();
        }
        #region 这部分应该被移到分班去,因为在创建学生信息时定学生还没入学,暂时放着方便测试
        #region 生成学号
        var academyCode = "01";
        var majorCode = "01";
        var classCode = "01";
        string teacherCode = $"{DateTime.Now.Year % 100}{academyCode}{majorCode}{classCode}";

        string? curMaxCode = _TeacherInfo.Table
            .Where(teacher => teacher.WorkCode != null && teacher.WorkCode.StartsWith(teacherCode))
            .OrderByDescending(teacher => teacher.WorkCode)
            .Select(teacher => teacher.WorkCode).FirstOrDefault();

        int incrementing = curMaxCode == null ? 01 : int.Parse(curMaxCode.Substring(curMaxCode.Length - 2)) + 1;
        teacherCode += (incrementing < 10 ? $"0{incrementing}" : incrementing.ToString());
        int teacherHash = new Random().Next(100, 10000);
        # endregion
        // 创建账号
        var user = await _userInfo.AddAsync
        (
            new UserInfo
            {
                Account = teacherHash.ToString(),
                Password = teacherDto.IdentityCode.Substring(teacherDto.IdentityCode.Length - 8),
            }
        );
        # endregion

        var teacher = await _TeacherInfo.AddAsync
        (
            new TeacherInfo
            {
                Name = teacherDto.Name,
                AcademyId = teacherDto.AcademyId,
                WorkCode = teacherHash.ToString(),
                IdentityCode = teacherDto.IdentityCode,
                UserId = user.Id // 这个也应该被移动到分班,不过就先这样放着测试
            }
        );

        await _userGroupUserInfo.AddAsync(new UserGroupUserInfo { UserId = user.Id, UserGroupId = group.Id });
        await _userGroupUserInfo.AddAsync(new UserGroupUserInfo { UserId = user.Id, UserGroupId = group2.Id });
        return new
        {
            Code = 1000,
            Data = new TeacherAccountDto
            {

                TeacherId = teacher.Id,
                UserId = user.Id,
                Account = user.Account,
                Password = user.Password,
                Name = teacherDto.Name,
                IdentityCode = teacherDto.IdentityCode,
                WorkCode = teacher.WorkCode,
                AcademyId = teacherDto.AcademyId
            },
            Msg = "添加教师成功"
        }.SerializeObject();
    }
    #endregion

    [HttpPut("/teacher/{TeacherId}")]
    # region 修改教师信息
    public async Task<string> UpdateTeacherInfo(Guid teacherId, [FromBody] UpdateTeacherDto TeacherDto)
    {
        var teacher = await _TeacherInfo.GetById(teacherId);
        if (teacher == null) return ReturnStudentIsEmpty(teacherId);

        var origin = await _updateServer.UpdateDataAsync(teacher, TeacherDto);
        if (teacher == origin)
        {
            return new
            {
                Code = 3003,
                Msg = "传入的信息与原本的信息一致或为空",
                Data = new
                {
                    Origin = origin,
                    Target = teacher
                }
            }.SerializeObject();
        }

        return new
        {
            Code = 1000,
            Msg = "修改教师信息成功",
            Data = new
            {
                Origin = origin,
                Updated = teacher
            }
        }.SerializeObject();
    }
    #endregion

    [HttpDelete("/teacher/{teacherId}")]
    # region 删除教师信息,并注销账号
    public async Task<string> DeleteTeacherInfo(Guid TeacherId)
    {
        var teacher = await _TeacherInfo.GetById(TeacherId);
        if (teacher == null) return ReturnStudentIsEmpty(TeacherId);

        // 移除学生账号(如果存在账号信息)
        Guid? teacherUserId = teacher.UserId;
        if (teacherUserId != null)
        {
            var shoudDel = _userGroupUserInfo.Table.Where(groupUser => groupUser.UserGroupId == teacherUserId);
            // 从用户组中移除该用户
            await _userGroupUserInfo.DeleteAsync(shoudDel);
            // 移除用户信息
            await _userInfo.DeleteAsync(teacherUserId.Value);
        }
        // 删除学生信息
        teacher = await _TeacherInfo.DeleteAsync(teacher);

        return new
        {
            Code = 1000,
            Msg = "删除成功",
            Data = teacher
        }.SerializeObject();
    }
    # endregion

    [NonAction]
    private string ReturnStudentIsEmpty(Guid teacherId)
    {
        return new
        {
            Code = 4000,
            Msg = $"Id为{teacherId}的教师不存在"
        }.SerializeObject();
    }
}