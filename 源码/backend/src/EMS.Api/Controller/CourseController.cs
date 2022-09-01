namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICRUDServer<CourseInfo, CreateCourseDto, UpdateCourseDto> _courseInfo;
    private readonly IGetDataServer<ClassCourse> _getClassCourseServer;
    private readonly IQueryHelper<CourseInfo> _queryHelper;

    public CourseController
    (
        ICRUDServer<CourseInfo, CreateCourseDto, UpdateCourseDto> courseInfo,
        IGetDataServer<ClassCourse> getClassCourseServer,
        IQueryHelper<CourseInfo> queryHelper
    )
    {
        _courseInfo = courseInfo;
        _getClassCourseServer = getClassCourseServer;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetCourseList() => (await _courseInfo.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{courseId}")]
    public async Task<string> GetCourseInfo(Guid courseId) => (await _courseInfo.GetDataById(courseId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateCourseInfo([FromBody] CreateCourseDto CourseDto) =>
        (await _courseInfo.CreateDataAsync(CourseDto)).SerializeObject();

    [HttpPut("{courseId}")]
    public async Task<string> UpdateCourseInfo(Guid courseId, [FromBody] UpdateCourseDto CourseDto) =>
        (await _courseInfo.UpdateDataAsync(courseId, CourseDto)).SerializeObject();

    [HttpDelete("{courseId}")]
    public async Task<string> DeleteCourseInfo(Guid courseId) => (await _courseInfo.DeleteDataAsync(courseId)).SerializeObject();

    private bool ValidateCourse(CourseInfo courseInfo)
    {
        var classId = _queryHelper.GetRequestQueryParam("classId");
        if (classId.IsNullOrEmpty())
        {
            return _queryHelper.ValidateData(courseInfo);
        }
        else
        {
            var classIdList = _getClassCourseServer
            .GetDataList(cc => cc.ClassId.Equals(new Guid(classId))
                && cc.CourseId.Equals(courseInfo.Id)
            , out int count);
            if (count > 0) return _queryHelper.ValidateData(courseInfo);
        }
        return false;
    }
}
