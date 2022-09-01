namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class MajorController : ControllerBase
{
    private readonly ICRUDServer<MajorInfo, CreateMajorDto, UpdateMajorDto> _majorInfo;
    private readonly IQueryHelper<MajorInfo> _queryHelper;

    public MajorController
    (
        ICRUDServer<MajorInfo, CreateMajorDto, UpdateMajorDto> majorInfo,
        IQueryHelper<MajorInfo> queryHelper
    )
    {
        _majorInfo = majorInfo;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetMajorList() =>
        (await _majorInfo.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{majorId}")]
    public async Task<string> GetMajorInfo(Guid majorId) =>
        (await _majorInfo.GetDataById(majorId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateMajorInfo([FromBody] CreateMajorDto majorDto) =>
        (await _majorInfo.CreateDataAsync(majorDto)).SerializeObject();

    [HttpPut("{majorId}")]
    public async Task<string> UpdateMajorInfo(Guid majorId, [FromBody] UpdateMajorDto majorDto) =>
        (await _majorInfo.UpdateDataAsync(majorId, majorDto)).SerializeObject();

    [HttpDelete("{majorId}")]
    public async Task<string> DeleteMajorInfo(Guid majorId) =>
        (await _majorInfo.DeleteDataAsync(majorId)).SerializeObject();
}
