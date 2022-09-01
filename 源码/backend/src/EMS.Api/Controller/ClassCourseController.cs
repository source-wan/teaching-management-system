namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class ClassCourseController : ControllerBase
{
    private readonly IRepository<TextBookInfo> _textBookInfo;
    private readonly IRepository<TeacherInfo> _teacherInfo;
    private readonly IRepository<ClassCourse> _classCourseInfo;

    private readonly IRepository<CourseInfo> _courseInfo;

    private readonly IRepository<CourseTextBook> _courseTextBook;

    // private readonly IRepository<CourseTeacher> _courseTeacher;
    private readonly ICRUDServer<ClassCourse, CreateClassCourseDto, UpdateClassCourseDto> _classCourse;
    private readonly IQueryHelper<ClassCourse> _queryHelper;

    public ClassCourseController

    (
        IRepository<TextBookInfo> textBookInfo,
        IRepository<TeacherInfo> teacherInfo,
        IRepository<CourseInfo> courseInfo,
        IRepository<ClassCourse> classCourseInfo,
        IRepository<CourseTextBook> courseTextBook,
        // IRepository<CourseTeacher> courseTeacher,
        ICRUDServer<ClassCourse, CreateClassCourseDto, UpdateClassCourseDto> classCourse,
        IQueryHelper<ClassCourse> queryHelper
    )
    {
        _textBookInfo = textBookInfo;
        _teacherInfo = teacherInfo;
        _courseInfo = courseInfo;
        // _courseTeacher = courseTeacher;
        _courseTextBook = courseTextBook;
        _classCourseInfo = classCourseInfo;
        _classCourse = classCourse;
        _queryHelper = queryHelper;
    }



    [HttpGet]
    public async Task<string> GetMajorList() =>(await _classCourse.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateClassCourse([FromBody] CreateClassCourseDto ClassDto) =>
        (await _classCourse.CreateDataAsync(ClassDto)).SerializeObject();

    [HttpPut("{classCourseId}")]
    public async Task<string> UpdateClassCourse(Guid classCourseId, [FromBody] UpdateClassCourseDto ClassDto) =>
        (await _classCourse.UpdateDataAsync(classCourseId, ClassDto)).SerializeObject();

    [HttpDelete("{classCourseId}")]
    public async Task<string> DeleteClassCourse(Guid classCourseId) => (await _classCourse.DeleteDataAsync(classCourseId)).SerializeObject();

    // [HttpGet]
    // public dynamic GetClassCourse(Guid classCourseId)
    // {
    //     var pageIndex = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
    //     var pageSize = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");
    //     int count;
    //     var data =
    //         GetClassCourseInfo
    //         (
    //             classCourse =>
    //                 classCourse.IsDeleted == false
    //                 && classCourse.IsActived == true
    //                 && _queryHelper.ValidateData(classCourse),
    //             out count,
    //             pageIndex, pageSize
    //         );

    //     return new
    //     {
    //         code = 1000,
    //         data = data,
    //         count = count,
    //         msg = "查找成功"
    //     }.SerializeObject();
    // }

    // [HttpGet("{classId}")]
    // public dynamic GetCourseList(Guid classId)
    // {
    //     var pageIndex = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
    //     var pageSize = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");
    //     int count;
    //     var data =
    //         getCourseList
    //         (
    //             classCourse =>
    //                 classCourse.IsDeleted == false
    //                 && classCourse.IsActived == true
    //                 && _queryHelper.ValidateData(classCourse),
    //             out count,
    //             pageIndex, pageSize
    //         );

    //     return new
    //     {
    //         code = 1000,
    //         data = data,
    //         count = count,
    //         msg = "查找成功"
    //     }.SerializeObject();
    // }

    // [HttpGet("/classcourse/teacher/{classId}")]
    // public dynamic GetTeacherList(Guid classId)
    // {
    //     var pageIndex = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
    //     var pageSize = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");
    //     int count;
    //     var data =
    //         getTeacherList
    //         (
    //             classCourse =>
    //                 classCourse.IsDeleted == false
    //                 && classCourse.IsActived == true
    //                 && _queryHelper.ValidateData(classCourse),
    //             out count,
    //             pageIndex, pageSize
    //         );

    //     return new
    //     {
    //         code = 1000,
    //         data = data,
    //         count = count,
    //         msg = "查找成功"
    //     }.SerializeObject();
    // }

    // [HttpGet("/classcourse/book/{classId}")]
    // public dynamic GetBookList(Guid classId)
    // {
    //     var pageIndex = int.Parse(_queryHelper.GetRequestQueryParam("index") ?? "1");
    //     var pageSize = int.Parse(_queryHelper.GetRequestQueryParam("size") ?? "5");
    //     int count;
    //     var data =
    //         getBookList
    //         (
    //             classCourse =>
    //                 classCourse.IsDeleted == false
    //                 && classCourse.IsActived == true
    //                 && _queryHelper.ValidateData(classCourse),
    //             out count,
    //             pageIndex, pageSize
    //         );

    //     return new
    //     {
    //         code = 1000,
    //         data = data,
    //         count = count,
    //         msg = "查找成功"
    //     }.SerializeObject();
    // }



    // [HttpGet("{classCourseId}")]
    // public async Task<string> GetClassCourseInfo(Guid classCourseId) => (await _classCourse.GetDataById(classCourseId)).SerializeObject();



    // [NonAction]
    // private IList<ClassCourseDto> GetClassCourseInfo(Func<ClassCourse, bool> validateFunc, out int count, int pageIndex = 1, int pageSize = 1)
    // {
    //     var data = _classCourseInfo.Table.Where(validateFunc);
    //     count = data.Count();
    //     // 使用连接查询获取到文章的相关数据并返回
    //     return
    //         (
    //             //获取课程名
    //             from classCourse in _classCourseInfo.GetByPageIndex(data.AsQueryable(), pageIndex, pageSize)
    //             join course in _courseInfo.Table on classCourse.CourseId equals course.Id into courseInfo
    //             from course in courseInfo.DefaultIfEmpty(_courseInfo.Table.First())

    //             //获取老师名
    //             join courseTeacher in _courseTeacher.Table on classCourse.CourseId equals courseTeacher.CourseId into courseTeacherInfo
    //             from courseTeacher in courseTeacherInfo.DefaultIfEmpty()
    //             join teacher in _teacherInfo.Table on courseTeacher.TeacherId equals teacher.Id into teacherInfo
    //             from teacher in teacherInfo.DefaultIfEmpty()

    //             join courseBook in _courseTextBook.Table on classCourse.CourseId equals courseBook.CourseId into courseBookInfo
    //             from courseBook in courseBookInfo.DefaultIfEmpty()
    //             join textBook in _textBookInfo.Table on courseBook.TextBookId equals textBook.Id into textBookInfo
    //             from textBook in textBookInfo.DefaultIfEmpty()

    //             let collectionCount = textBookInfo.Count()
    //             select new ClassCourseDto
    //             {

    //                 Id = classCourse.Id,
    //                 Term = classCourse.Term,
    //                 courseName = course.CourseName,
    //                 courseTeacherName = teacher.Name,
    //                 courseTextBook = textBook.BookName
    //             }
    //         ).ToList();

    // }


    // [NonAction]
    // private IList<CourseListDto> getCourseList(Func<ClassCourse, bool> validateFunc, out int count, int pageIndex = 1, int pageSize = 1)
    // {
    //     var data = _classCourseInfo.Table.Where(validateFunc);
    //     count = data.Count();
    //     // 使用连接查询获取到文章的相关数据并返回
    //     return
    //         (
    //             from classCourse in _classCourseInfo.GetByPageIndex(data.AsQueryable(), pageIndex, pageSize)
    //             join course in _courseInfo.Table on classCourse.CourseId equals course.Id into courseInfo
    //             from course in courseInfo.DefaultIfEmpty(_courseInfo.Table.First())



    //             let collectionCount = courseInfo.Count()
    //             select new CourseListDto
    //             {

    //                 Id = course.Id,
    //                 CourseName = course.CourseName
    //             }
    //         ).ToList();

    // }
    // [NonAction]
    // private IList<TeacherListDto> getTeacherList(Func<ClassCourse, bool> validateFunc, out int count, int pageIndex = 1, int pageSize = 1)
    // {
    //     // 获取班级所有的课程的教师
    //     // 获取班级 --> 获取班级的课程 --> 获取课程对应的教师 （ps: 教师课程表的 id --> 教师id
    //     // 教师课程表 --> 教师id <-- 课程的id
    //     // coureseId <-- teacherCourseId _table.GetById（teacherCourseId).teacher
    //     var data = _classCourseInfo.Table.Where(validateFunc);
    //     count = data.Count();
    //     // 使用连接查询获取到文章的相关数据并返回
    //     return
    //         (
    //             from classCourse in _classCourseInfo.GetByPageIndex(data.AsQueryable(), pageIndex, pageSize)
    //             join courseTeacher in _courseTeacher.Table on classCourse.CourseId equals courseTeacher.CourseId into courseTeacherInfo
    //             from courseTeacher in courseTeacherInfo.DefaultIfEmpty()
    //             join teacher in _teacherInfo.Table on courseTeacher.TeacherId equals teacher.Id into teacherInfo
    //             from teacher in teacherInfo.DefaultIfEmpty()

    //             let collectionCount = teacherInfo.Count()
    //             select new TeacherListDto
    //             {

    //                 Id = teacher.Id,
    //                 TeacherName = teacher.Name
    //             }
    //         ).ToList();

    // }
    //     [NonAction]
    // private IList<BookListDto> getBookList(Func<ClassCourse, bool> validateFunc, out int count, int pageIndex = 1, int pageSize = 1)
    // {
    //     var data = _classCourseInfo.Table.Where(validateFunc);
    //     count = data.Count();
    //     // 使用连接查询获取到文章的相关数据并返回
    //     return
    //         (
    //             from classCourse in _classCourseInfo.GetByPageIndex(data.AsQueryable(), pageIndex, pageSize)
    //             join courseBook in _courseTextBook.Table on classCourse.CourseId equals courseBook.CourseId into courseBookInfo
    //             from courseBook in courseBookInfo.DefaultIfEmpty()
    //             join textBook in _textBookInfo.Table on courseBook.TextBookId equals textBook.Id into textBookInfo
    //             from textBook in textBookInfo.DefaultIfEmpty()

    //             let collectionCount = textBookInfo.Count()
    //             select new BookListDto
    //             {

    //                 Id = textBook.Id,
    //                 BookName = textBook.BookName
    //             }
    //         ).ToList();

    // }
}
