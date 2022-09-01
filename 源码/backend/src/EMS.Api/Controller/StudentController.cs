namespace EMS.Api.Controller;
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IRepository<UserInfo> _userInfo;
    private readonly IRepository<StudentInfo> _studentInfo;
    private readonly IRepository<RoleInfo> _roleInfo;
    private readonly IRepository<UserGroupInfo> _userGroupInfo;
    private readonly IRepository<UserGroupUserInfo> _userGroupUserInfo;

    private readonly IRepository<StudentMajor> _studentMajor;
    private readonly IIdentity _identity;
    private readonly IQueryHelper<StudentInfo> _queryHelper;
    private readonly IUpdateDataServer<StudentInfo, UpdateStudentDto> _updateServer;

    public StudentController
    (IRepository<StudentMajor> studentMajor,
        IRepository<UserInfo> userInfo,
        IRepository<RoleInfo> roleInfo,
        IRepository<UserGroupInfo> userGroupInfo,
        IRepository<UserGroupUserInfo> userGroupUserInfo,
        IRepository<StudentInfo> appStudent,
        IIdentity identity,
        IQueryHelper<StudentInfo> queryHelper,
        IUpdateDataServer<StudentInfo, UpdateStudentDto> updateServer
    )
    {
        _studentMajor = studentMajor;
        _userInfo = userInfo;
        _roleInfo = roleInfo;
        _userGroupInfo = userGroupInfo;
        _userGroupUserInfo = userGroupUserInfo;
        _studentInfo = appStudent;
        _identity = identity;
        _queryHelper = queryHelper;
        _updateServer = updateServer;
    }
    // public  IEnumerable<StudentInfo> GetStudentListByStudentId(Guid studentId)
    // {
    //     var studentList = _getClassStudentServer.GetDataList(cs => cs.ClassId.Equals(studentId), out int stuIdCount).Select(cs => cs.StudentId).ToList();
    //     IEnumerable<StudentInfo> studentInfos = new List<StudentInfo>();
    //     int stuCount = 0;
    //     if (stuIdCount > 0)
    //     {
    //         studentInfos = _getStudentInfoServer
    //         .GetDataList(student => studentList.Contains(student.Id) && _queryHelperForStudentInfo.ValidateData(student)
    //         , out stuCount);
    //     }

    //     return studentInfos;
    // }

    [HttpGet("/student/{studentId}")]
    # region 获取学生详细信息
    public async Task<string> GetStudentInfo(Guid studentId)
    {
        var student = await _studentInfo.GetById(studentId);
        if (student == null) return ReturnStudentIsEmpty(studentId);

        return new
        {
            Code = 1000,
            Data = student,
            Msg = "获取学生信息成功"
        }.SerializeObject();
    }
    #endregion

    [HttpGet("/student")]
    [Authorize(Roles = "admin,teacher,instructor,student")]
    # region 获取学生列表
    public string GetStudentList()
    {
        int index = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
        int size = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");

        var students = _studentInfo.Table.Where(_queryHelper.ValidateData);
        var count = students.Count();

        if (count > 0)
        {
            if (Request.Query.ContainsKey("orderbydesc") && (!Request.Query.ContainsKey("orderby")))
            {
                students = students
                .OrderByDescending(_queryHelper.SortByRequestParam)
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            }
            else
            {
                students = students
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
                Data = students,
                Count = count
            },
            Msg = "获取学生花名册成功"
        }.SerializeObject();
    }
    #endregion

    [HttpPost("/student")]
    [Authorize(Roles = "admin,teacher,instructor")]
    # region 添加学生信息,并注册账号
    public async Task<string> CreateStudentInfoAsync([FromBody] CreateStudentDto studentDto)
    {
        var group = _identity.GetUserGroupInfo(RoleType.Student);
        // 检查学生组是否已经被创建,如果没有则响应相关信息
        if (group == null)
        {
            return new
            {
                Code = "4000",
                Msg = "学生组尚未被创建,请创建后重试"
            }.SerializeObject();
        }
        // 必须的信息是否为空,如果为空则响应相关信息
        if (studentDto.StudentName.IsNullOrEmpty() || studentDto.IdentityCode.IsNullOrEmpty())
        {
            return new
            {
                Code = 3003,
                Msg = @$"学生姓名:{studentDto.StudentName}或(及)身份证号:{studentDto.IdentityCode} 为空"
            }.SerializeObject();
        }

        var student = await _studentInfo.AddAsync
        (
            new StudentInfo
            {
                Name = studentDto.StudentName,
                IdentityCode = studentDto.IdentityCode,
                Gender = studentDto.Gender,
                MajorId = studentDto.MajorId
            }
        );

        return new
        {
            Code = 1000,
            Data = student,
            Msg = "添加学生成功"
        }.SerializeObject();
    }
    #endregion

    [HttpPut("/student/{studentId}")]
    [Authorize(Roles = "admin,teacher,instructor,student")]
    # region 修改学生信息
    public async Task<string> UpdateStudentInfo(Guid studentId, [FromBody] UpdateStudentDto studentDto)
    {
        var student = await _studentInfo.GetById(studentId);
        if (student == null) return ReturnStudentIsEmpty(studentId);

        var origin = await _updateServer.UpdateDataAsync(student, studentDto);
        if (student == origin)
        {
            return new
            {
                Code = 3003,
                Msg = "传入的信息与原本的信息一致或为空",
                Data = new
                {
                    Origin = origin,
                    Target = student
                }
            }.SerializeObject();
        }

        return new
        {
            Code = 1000,
            Msg = "修改学生信息成功",
            Data = new
            {
                Origin = origin,
                Updated = student
            }
        }.SerializeObject();
    }
    #endregion

    [HttpDelete("/student/{studentId}")]
    [Authorize(Roles = "admin,teacher,instructor")]
    # region 删除学生信息,并注销账号
    public async Task<string> DeleteStudentInfo(Guid studentId)
    {
        var student = await _studentInfo.GetById(studentId);
        if (student == null) return ReturnStudentIsEmpty(studentId);

        // 移除学生账号(如果存在账号信息)
        Guid? studentUserId = student.UserId;
        if (studentUserId != null)
        {
            var shoudDel = _userGroupUserInfo.Table.Where(groupUser => groupUser.UserGroupId == studentUserId);
            // 从用户组中移除该用户
            await _userGroupUserInfo.DeleteAsync(shoudDel);
            // 移除用户信息
            await _userInfo.DeleteAsync(studentUserId.Value);
        }
        // 删除学生信息
        student = await _studentInfo.DeleteAsync(student);

        return new
        {
            Code = 1000,
            Msg = "删除成功",
            Data = student
        }.SerializeObject();
    }
    # endregion

    [NonAction]
    private string ReturnStudentIsEmpty(Guid studentId)
    {
        return new
        {
            Code = 4000,
            Msg = $"Id为{studentId}的学生不存在"
        }.SerializeObject();
    }
}