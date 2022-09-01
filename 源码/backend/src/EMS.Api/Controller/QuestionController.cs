namespace EMS.Api.Controller;
[ApiController]
[Route("[controller]")]
public class QuestionController : ControllerBase
{
    private readonly ICRUDServer<QuestionInfo, CreateQuestionDto, UpdateQuestionDto> _questionInfo;
    private readonly IQueryHelper<QuestionInfo> _queryHelper;

    public QuestionController
    (
        ICRUDServer<QuestionInfo, CreateQuestionDto, UpdateQuestionDto> questionInfo,
        IQueryHelper<QuestionInfo> queryHelper
    )
    {
        _questionInfo = questionInfo;
        _queryHelper = queryHelper;
    }

    [HttpGet]
    public async Task<string> GetClassList() => (await _questionInfo.GetData(_queryHelper.ValidateData)).SerializeObject();

    [HttpGet("{QuestionId}")]
    public async Task<string> GetquestionInfo(Guid QuestionId) => (await _questionInfo.GetDataById(QuestionId)).SerializeObject();

    [HttpPost]
    public async Task<string> CreatequestionInfo([FromBody] CreateQuestionDto QuestionDto) =>
        (await _questionInfo.CreateDataAsync(QuestionDto)).SerializeObject();

    [HttpPut("{QuestionId}")]
    public async Task<string> UpdatequestionInfo(Guid QuestionId, [FromBody] UpdateQuestionDto QuestionDto) =>
        (await _questionInfo.UpdateDataAsync(QuestionId, QuestionDto)).SerializeObject();

    [HttpDelete("{QuestionId}")]
    public async Task<string> DeletequestionInfo(Guid QuestionId) => (await _questionInfo.DeleteDataAsync(QuestionId)).SerializeObject();
}