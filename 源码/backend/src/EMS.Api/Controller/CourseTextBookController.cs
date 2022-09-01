namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class CourseTextBookController : ControllerBase
{
    private readonly ICRUDServer<CourseTextBook, CreateCourseTextBookDto, UpdateCourseTextBookDto> _CourseTextBook;
    private readonly IQueryHelper<CourseTextBook> _queryHelper;

    public CourseTextBookController
    (
        ICRUDServer<CourseTextBook, CreateCourseTextBookDto, UpdateCourseTextBookDto> CourseTextBook,
        IQueryHelper<CourseTextBook> queryHelper
    )
    {
        _CourseTextBook = CourseTextBook;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetClassList() => (await _CourseTextBook.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{CourseTextBookId}")]
    public async Task<string> GetCourseTextBook(Guid CourseTextBookId) => (await _CourseTextBook.GetDataById(CourseTextBookId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateCourseTextBook([FromBody] CreateCourseTextBookDto ClassDto) =>
        (await _CourseTextBook.CreateDataAsync(ClassDto)).SerializeObject();

    [HttpPut("{CourseTextBookId}")]
    public async Task<string> UpdateCourseTextBook(Guid CourseTextBookId, [FromBody] UpdateCourseTextBookDto ClassDto) =>
        (await _CourseTextBook.UpdateDataAsync(CourseTextBookId, ClassDto)).SerializeObject();

    [HttpDelete("{CourseTextBookId}")]
    public async Task<string> DeleteCourseTextBook(Guid CourseTextBookId) => (await _CourseTextBook.DeleteDataAsync(CourseTextBookId)).SerializeObject();
}
