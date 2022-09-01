namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class ScoreController : ControllerBase
{
    private readonly ICRUDServer<ScoreInfo, CreateScoreDto, UpdateScoreDto> _scoreInfoServer;
    private readonly IGetDataServer<TeacherInfo> _getTeacherServer;
    private readonly IGetDataServer<StudentInfo> _getStudentServer;
    private readonly IGetDataServer<ScoreInfo> _getScoreServer;
    private readonly IGetDataServer<CourseInfo> _getCourseInfoServer;
    private readonly IGetDataServer<ClassCourse> _getClassCourseServer;
    private readonly IGetDataServer<ClassStudent> _getClassStudentServer;
    private readonly IQueryHelper<CourseInfo> _queryHelperForCourseInfo;
    private readonly IUserSession _user;
    private readonly ResponseDto _response = new ResponseDto();

    public ScoreController(ICRUDServer<ScoreInfo, CreateScoreDto, UpdateScoreDto> scoreInfoServer,
        IGetDataServer<CourseInfo> getCourseInfoServer,
        IUserSession user,
        IGetDataServer<ScoreInfo> getScoreServer,
        IQueryHelper<CourseInfo> queryHelperForCourseInfo,
        IGetDataServer<ClassCourse> getClassCourseServer,
        IGetDataServer<ClassStudent> getClassStudentServer,
        IGetDataServer<TeacherInfo> getTeacherServer,
        IGetDataServer<StudentInfo> getStudentServer)
    {
        _scoreInfoServer = scoreInfoServer;
        _getCourseInfoServer = getCourseInfoServer;
        _user = user;
        _getScoreServer = getScoreServer;
        _queryHelperForCourseInfo = queryHelperForCourseInfo;
        _getClassCourseServer = getClassCourseServer;
        _getClassStudentServer = getClassStudentServer;
        _getTeacherServer = getTeacherServer;
        _getStudentServer = getStudentServer;
    }

    [HttpGet]
    [Authorize(Roles = "student")]
    public async Task<string> GetCurrentUserScore()
    {
        var studentId = _getStudentServer.GetDataList(student => student.UserId.Equals(_user.Id), out int studentCount)
        .Select(student => student.Id).FirstOrDefault();
        if (studentCount == 0) return _response.DataIsEmpty(studentId).SerializeObject();
        var scores = _getScoreServer.GetDataList(score => score.StudentId.Equals(studentId) && score.IsActived && !score.IsDeleted, out int scoreCount).ToList();
        if (scoreCount == 0)
            return _response.CustomResponse(ResponseCode.Success, "你还没有参加过考试", scores).SerializeObject();

        var result = await Task.Run(() =>
            from score in scores
            join course in _getCourseInfoServer.GetDataList(_queryHelperForCourseInfo.ValidateData, out int courseCount)
            on score.CourseId equals course.Id
            select new CurrentStudentScoreDto { Score = score.Score, CourseName = course.CourseName, Credit = course.Credit ,Term=score.Term});

        return _response.CustomResponse(ResponseCode.Success, "获取当前学生成绩成功", result).SerializeObject();
    }

    [HttpPost("{classId}")]
    [Authorize(Roles = "teacher")]
    public async Task<string> CreateScoreInfoAsync(Guid classId, [FromBody] IEnumerable<CreateScoreDto> scoreDtos)
    {
        var teacherId = _getTeacherServer
            .GetDataList(teacher => teacher.UserId.Equals(_user.Id), out int teacherCount)
            .Select(teacher => teacher.Id).FirstOrDefault();

        if (teacherId == Guid.Empty) return _response.DataIsEmpty(teacherId).SerializeObject();

        _getClassCourseServer.GetDataList(classInfo => classInfo.TeacherId.Equals(teacherId), out int classCount);
        if (classCount == 0) return _response.CustomResponse(ResponseCode.Fail, "不能给其它班级的学生打分").SerializeObject();

        var students = 
            _getClassStudentServer.GetDataList(cs => cs.ClassId == classId, out int studentCount)
            .OrderBy(cs => cs.JoinTime).ToList();
        students = students.Where((student, index) => index == students.LastIndexOf(student)).ToList();

        if (!
        students.Any()) return _response.CustomResponse(ResponseCode.Fail, "班级不存在,或该班级下没有学生").SerializeObject();

        var notExistsStudent =  _getClassStudentServer.GetDataList(cs =>
                !scoreDtos.Select(scoreDto => scoreDto.StudentId).Contains(cs.StudentId),
                out int notExistsCount);
        if (notExistsCount > 0) return _response.DataIsEmpty(notExistsStudent.First().Id).SerializeObject();

        // 判断是否有为空的字段
        foreach (var score in scoreDtos)
        {
            foreach (var scoreProp in score.GetType().GetProperties())
            {
                if (scoreProp.IsNullOrEmpty())
                    return _response.CreateDataFail(scoreProp.Name, score).SerializeObject();
            }
        }
        return (await _scoreInfoServer.CreateDataAsync(scoreDtos)).SerializeObject();
    }
}
