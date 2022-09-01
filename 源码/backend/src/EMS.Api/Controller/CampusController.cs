namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class CampusController : ControllerBase
{
    private readonly ICRUDServer<CampusInfo, CreateCampusDto, UpdateCampusDto> _campusInfo;
    private readonly IQueryHelper<CampusInfo> _queryHelper;

    public CampusController
    (
        ICRUDServer<CampusInfo, CreateCampusDto, UpdateCampusDto> campusInfo,
        IQueryHelper<CampusInfo> queryHelper
    )
    {
        _campusInfo = campusInfo;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetCampusList() => (await _campusInfo.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{campusId}")]
    public async Task<string> GetCampusInfo(Guid campusId) => (await _campusInfo.GetDataById(campusId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateCampusInfo([FromBody] CreateCampusDto CampusDto) => 
        (await _campusInfo.CreateDataAsync(CampusDto)).SerializeObject();

    [HttpPut("{campusId}")]
    public async Task<string> UpdateCampusInfo(Guid campusId, [FromBody] UpdateCampusDto CampusDto) => 
        (await _campusInfo.UpdateDataAsync(campusId, CampusDto)).SerializeObject();

    [HttpDelete("{campusId}")]
    public async Task<string> DeleteCampusInfo(Guid campusId) => (await _campusInfo.DeleteDataAsync(campusId)).SerializeObject();
}
