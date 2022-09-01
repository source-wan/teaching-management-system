namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class SurveyQuestionController : ControllerBase
{
    private readonly ICRUDServer<SurveyQuestion, CreateSurveyQuestionDto, UpdateSurveyQuestionDto> _SurveyQuestion;
    private readonly IQueryHelper<SurveyQuestion> _queryHelper;

    public SurveyQuestionController
    (
        ICRUDServer<SurveyQuestion, CreateSurveyQuestionDto, UpdateSurveyQuestionDto> SurveyQuestion,
        IQueryHelper<SurveyQuestion> queryHelper
    )
    {
        _SurveyQuestion = SurveyQuestion;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetClassList() => (await _SurveyQuestion.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{SurveyQuestionId}")]
    public async Task<string> GetSurveyQuestion(Guid SurveyQuestionId) => (await _SurveyQuestion.GetDataById(SurveyQuestionId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreateSurveyQuestion([FromBody] CreateSurveyQuestionDto SurveyQuestionDto) =>
        (await _SurveyQuestion.CreateDataAsync(SurveyQuestionDto)).SerializeObject();

    [HttpPut("{SurveyQuestionId}")]
    public async Task<string> UpdateSurveyQuestion(Guid SurveyQuestionId, [FromBody] UpdateSurveyQuestionDto SurveyQuestionDto) =>
        (await _SurveyQuestion.UpdateDataAsync(SurveyQuestionId, SurveyQuestionDto)).SerializeObject();

    [HttpDelete("{SurveyQuestionId}")]
    public async Task<string> DeleteSurveyQuestion(Guid SurveyQuestionId) => (await _SurveyQuestion.DeleteDataAsync(SurveyQuestionId)).SerializeObject();
}