namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class ClassController : ControllerBase
{
    private readonly ICRUDServer<ClassInfo, CreateClassDto, UpdateClassDto> _classInfo;
    private readonly IQueryHelper<ClassInfo> _queryHelper;

    public ClassController
    (
        ICRUDServer<ClassInfo, CreateClassDto, UpdateClassDto> classInfo,
        IQueryHelper<ClassInfo> queryHelper
    )
    {
        _classInfo = classInfo;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetClassList() => (await _classInfo.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{classId}")]
    public async Task<string> GetClassInfo(Guid classId) => (await _classInfo.GetDataById(classId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateClassInfo([FromBody] CreateClassDto ClassDto) =>
        (await _classInfo.CreateDataAsync(ClassDto)).SerializeObject();

    [HttpPut("{classId}")]
    public async Task<string> UpdateClassInfo(Guid classId, [FromBody] UpdateClassDto ClassDto) =>
        (await _classInfo.UpdateDataAsync(classId, ClassDto)).SerializeObject();

    [HttpDelete("{classId}")]
    public async Task<string> DeleteClassInfo(Guid classId) => (await _classInfo.DeleteDataAsync(classId)).SerializeObject();
}
