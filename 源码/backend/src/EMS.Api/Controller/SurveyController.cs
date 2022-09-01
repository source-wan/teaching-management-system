namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class SurveyController : ControllerBase
{
    private readonly ICRUDServer<SurveyInfo, CreateSurveyDto, UpdateSurveyDto> _SurveyInfo;
    private readonly IQueryHelper<SurveyInfo> _queryHelper;

    public SurveyController
    (
        ICRUDServer<SurveyInfo, CreateSurveyDto, UpdateSurveyDto> SurveyInfo,
        IQueryHelper<SurveyInfo> queryHelper
    )
    {
        _SurveyInfo = SurveyInfo;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetClassList() => (await _SurveyInfo.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{SurveyId}")]
    public async Task<string> GetSurveyInfo(Guid SurveyId) => (await _SurveyInfo.GetDataById(SurveyId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateSurveyInfo([FromBody] CreateSurveyDto SurveyDto) =>
        (await _SurveyInfo.CreateDataAsync(SurveyDto)).SerializeObject();

    [HttpPut("{SurveyId}")]
    public async Task<string> UpdateSurveyInfo(Guid SurveyId, [FromBody] UpdateSurveyDto SurveyDto) =>
        (await _SurveyInfo.UpdateDataAsync(SurveyId, SurveyDto)).SerializeObject();

    [HttpDelete("{SurveyId}")]
    public async Task<string> DeleteSurveyInfo(Guid SurveyId) => (await _SurveyInfo.DeleteDataAsync(SurveyId)).SerializeObject();
}