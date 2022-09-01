namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class ClassStudentController : ControllerBase
{
    private readonly IGetDataServer<ClassStudent> _getClassStudentServer;
    private readonly IGetDataServer<ClassInfo> _getClassInfoServer;
    private readonly IGetDataServer<RoleInfo> _getRoleInfoServer;
    private readonly IGetDataServer<UserGroupInfo> _getUserGroupInfoServer;
    private readonly IGetDataServer<StudentInfo> _getStudentInfoServer;
    private readonly IGetDataServer<MajorInfo> _getMajorServer;
    private readonly IGetDataServer<AcademyInfo> _getAcademyServer;
    private readonly ICreateDataServer<UserInfo> _createUserServer;
    private readonly ICreateDataServer<UserGroupUserInfo> _createUserGroupUserServer;
    private readonly ICreateDataServer<ClassStudent> _createClassStudentServer;
    private readonly IUpdateDataServer<StudentInfo> _updateStudentServer;
    private readonly IQueryHelper<StudentInfo> _queryHelperForStudentInfo;
    private readonly IIdentity _identity;
    private readonly ResponseDto _response = new ResponseDto();

    public ClassStudentController
    (
        IGetDataServer<ClassStudent> getClassStudentServer,
        IGetDataServer<ClassInfo> getClassInfoServer,
        IGetDataServer<RoleInfo> getRoleInfoServer,
        IGetDataServer<UserGroupInfo> getUserGroupInfoServer,
        IGetDataServer<StudentInfo> getStudentInfoServer,
        IGetDataServer<MajorInfo> getMajorServer,
        IGetDataServer<AcademyInfo> getAcademyServer,
        ICreateDataServer<UserInfo> createUserServer,
        ICreateDataServer<UserGroupUserInfo> createUserGroupUserServer,
        ICreateDataServer<ClassStudent> createClassStudentServer,
        IUpdateDataServer<StudentInfo> updateStudentServer,
        IQueryHelper<StudentInfo> queryHelperForStudentInfo,
        IIdentity identity
    )
    {
        _getClassStudentServer = getClassStudentServer;
        _getClassInfoServer = getClassInfoServer;
        _getMajorServer = getMajorServer;
        _getAcademyServer = getAcademyServer;
        _getRoleInfoServer = getRoleInfoServer;
        _getUserGroupInfoServer = getUserGroupInfoServer;
        _getStudentInfoServer = getStudentInfoServer;
        _createUserServer = createUserServer;
        _createUserGroupUserServer = createUserGroupUserServer;
        _createClassStudentServer = createClassStudentServer;
        _updateStudentServer = updateStudentServer;
        _queryHelperForStudentInfo = queryHelperForStudentInfo;
        _identity = identity;
    }





    [HttpPost("{classId}")]
    public async Task<string> AddStudentToClassAsync(Guid classId, [FromBody] IEnumerable<AddStudentToClassDto> addStudentToClassDtos)
    {
        var classInfo = await _getClassInfoServer.GetDataById(classId);
        if (classInfo == null)
        {
            return new ResponseDto
            {
                Code = ResponseCode.NotFound,
                Message = $"Id为{classId}的班级不存在"
            }.SerializeObject();
        }

        var group = _identity.GetUserGroupInfo(RoleType.Student);
        // 检查学生组是否已经被创建,如果没有则响应相关信息
        if (group == null)
        {
            return new ResponseDto
            {
                Code = ResponseCode.NotFound,
                Message = "学生组尚未被创建,请创建后重试"
            }.SerializeObject();
        }

        var reqStudentId = addStudentToClassDtos.Select(student => student.StudentId);
        var repeatStudentIdArr = _getClassStudentServer.GetDataList(cs => reqStudentId.Contains(cs.StudentId)
        && cs.ClassId.Equals(classId) && cs.IsActived && !cs.IsDeleted, out int repeatCount)
        .Select(x => x.StudentId);
        if (repeatCount > 0) return _response.CustomResponse(ResponseCode.Fail, "不能向同一个班级插入重复的学生", repeatStudentIdArr).SerializeObject();

        var studentList = _getStudentInfoServer.GetDataList(student => reqStudentId.Contains(student.Id), out int count).ToList();
        if (count < addStudentToClassDtos.Count())
        {
            var studentId = studentList.Select(student => student.Id);
            var notExistsStudentId = addStudentToClassDtos.Where(addStudentInfo => !studentId.Contains(addStudentInfo.StudentId)).Select(student => student.StudentId);
            return _response.CustomResponse
            (
                ResponseCode.NotFound,
                "Id如下的学生不存在",
                notExistsStudentId
            ).SerializeObject();
        }

        #region 生成学号
        var majorInfo = await _getMajorServer.GetDataById(classInfo.MajorId);
        if (majorInfo == null) return new ResponseDto { Code = ResponseCode.Fail, Message = "专业信息不存在，请联系管理员" }.SerializeObject();
        var academyInfo = await _getAcademyServer.GetDataById(majorInfo.AcademyId);
        if (academyInfo == null) return new ResponseDto { Code = ResponseCode.Fail, Message = "院系信息不存在，请联系管理员" }.SerializeObject();
        string academyCode = academyInfo.AcademyCode < 10 ? $"0{academyInfo.AcademyCode}" : academyInfo.AcademyCode.ToString();
        string majorCode = majorInfo.MajorCode < 10 ? $"0{majorInfo.MajorCode}" : majorInfo.MajorCode.ToString();
        string classCode = classInfo.ClassCode < 10 ? $"0{classInfo.ClassCode}" : classInfo.ClassCode.ToString();

        string stuCode = $"{DateTime.Now.Year % 100}{academyCode}{majorCode}{classCode}";

        string? curMaxCode = _getStudentInfoServer.GetDataList(stu => stu.IsActived && !stu.IsDeleted, out int stuCount)
            .Where(student => student.StudentCode != null && student.StudentCode.StartsWith(stuCode))
            .OrderByDescending(student => student.StudentCode)
            .Select(student => student.StudentCode).FirstOrDefault();

        var studentCodeArr = new string[count];
        for (int i = 0; i < count; i++)
        {
            int incrementing = curMaxCode == null ? 1 : int.Parse(curMaxCode.Substring(curMaxCode.Length - 2)) + 1;
            studentCodeArr[i] = stuCode + (incrementing < 10 ? $"0{incrementing}" : incrementing.ToString());
            curMaxCode = studentCodeArr[i];
        }
        #endregion

        // 创建账号
        var userArr = new UserInfo[count];

        for (int i = 0; i < count; i++)
        {
            var curStudent = studentList.ElementAt(i);
            var account = studentCodeArr[i];
            if (account == null) return _response.CustomResponse(ResponseCode.Fail, "学号生成错误", curStudent).SerializeObject();

            var password = curStudent.IdentityCode.Substring(curStudent.IdentityCode.Length - 6);
            userArr[i] = new UserInfo
            {
                Account = account,
                Password = password,
            };

        }
        var insertedUser = await _createUserServer.CreateDataAsync(userArr);
        var groupUserInfoList = new List<UserGroupUserInfo>();

        foreach (var user in insertedUser)
        {
            if (user == null) return _response.CustomResponse(ResponseCode.Fail, "创建用户失败，请联系站长", user).SerializeObject();

            groupUserInfoList.Add(
                new UserGroupUserInfo
                {
                    UserId = user.Id,
                    UserGroupId = group.Id
                }
            );
        }

        var insertedGroupUser = await _createUserGroupUserServer.CreateDataAsync(groupUserInfoList);
        var dataList = new StudentAccountDto[count];
        var stuArr = studentList.ToArray();
        var updateStudentTargetArr = new StudentInfo[count];
        var classStudentArr = new ClassStudent[count];
        for (int i = 0; i < count; i++)
        {
            dataList[i] = new StudentAccountDto
            {
                StudentCode = userArr[i].Account,
                StudentName = stuArr[i].Name,
                IdentityCode = stuArr[i].IdentityCode,
                Account = userArr[i].Account,
                Password = userArr[i].Password,
                UserId = userArr[i].Id,
                StudentId = stuArr[i].Id
            };

            updateStudentTargetArr[i] = new StudentInfo
            {
                UserId = userArr[i].Id,
                StudentCode = userArr[i].Account,
                MajorId = stuArr[i].MajorId
            };

            classStudentArr[i] = new ClassStudent
            {
                ClassId = classId,
                StudentId = stuArr[i].Id,
                JoinTime = new DateTime(1970, 1, 1, 8, 0, 0)
                .AddSeconds(addStudentToClassDtos.ElementAt(i).JoinTime)
                .ToUniversalTime()
            };
        }
        await _updateStudentServer.UpdateDataAsync(studentList, updateStudentTargetArr);
        await _createClassStudentServer.CreateDataAsync(classStudentArr);
        return _response.CreateDataSuccess(dataList).SerializeObject();

    }

    /// <summary>
    /// 根据班级Id获取学生数据
    /// </summary>
    /// <param name="classId"></param>
    /// <returns></returns>
    [HttpGet("{classId}")]
    public string GetStudentListByClassId(Guid classId)
    {
        var studentId = _getClassStudentServer.GetDataList(cs => cs.ClassId.Equals(classId), out int stuIdCount).Select(cs => cs.StudentId).ToList();
        IEnumerable<StudentInfo> studentInfos = new List<StudentInfo>();
        int stuCount = 0;
        if (stuIdCount > 0)
        {
            studentInfos = _getStudentInfoServer
            .GetDataList(student => studentId.Contains(student.Id) && _queryHelperForStudentInfo.ValidateData(student)
            , out stuCount);
        }

        return new ResponseDto().ReturnDataInfoWithCount(studentInfos, stuCount).SerializeObject();
    }


    [HttpGet("/classstudent/student/{studentId}")]
    public string GetStudentListByStudentId(Guid studentId)
    {
        var studentList = _getClassStudentServer.GetDataList(cs => cs.StudentId.Equals(studentId), out int stuIdCount).Select(cs => cs.ClassId).ToList();
        IEnumerable<ClassInfo> classInfos = new List<ClassInfo>();
        int stuCount = 0;
        if (stuIdCount > 0)
        {

            classInfos = _getClassInfoServer
            .GetDataList(classInfo => studentList.Contains(classInfo.Id)
            , out stuCount).ToList();
        }
        return new ResponseDto().ReturnDataInfoWithCount(classInfos, stuCount).SerializeObject();
    }

}