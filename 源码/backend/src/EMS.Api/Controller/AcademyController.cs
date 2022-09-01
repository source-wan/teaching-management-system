namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class AcademyController : ControllerBase
{
    private readonly ICRUDServer<AcademyInfo, CreateAcademyDto, UpdateAcademyDto> _academyInfo;
    private readonly IQueryHelper<AcademyInfo> _queryHelper;

    public AcademyController
    (
        ICRUDServer<AcademyInfo, CreateAcademyDto, UpdateAcademyDto> academyInfo,
        IQueryHelper<AcademyInfo> queryHelper
    )
    {
        _academyInfo = academyInfo;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetAcademyList() => (await _academyInfo.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{academyId}")]
    public async Task<string> GetAcademyInfo(Guid academyId) => (await _academyInfo.GetDataById(academyId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateAcademyInfo([FromBody] CreateAcademyDto AcademyDto) => 
        (await _academyInfo.CreateDataAsync(AcademyDto)).SerializeObject();

    [HttpPut("{academyId}")]
    public async Task<string> UpdateAcademyInfo(Guid academyId, [FromBody] UpdateAcademyDto AcademyDto) => 
        (await _academyInfo.UpdateDataAsync(academyId, AcademyDto)).SerializeObject();

    [HttpDelete("{academyId}")]
    public async Task<string> DeleteAcademyInfo(Guid academyId) => (await _academyInfo.DeleteDataAsync(academyId)).SerializeObject();
}
