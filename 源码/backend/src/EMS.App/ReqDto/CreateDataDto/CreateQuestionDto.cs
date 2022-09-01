namespace EMS.App.ReqDto.CreateDataDto;
public class CreateQuestionDto : Base.CreateDataDto
{
    public Guid SurveyId { get; set; }

    public string QuestionName { get; set; } = null!;
}
