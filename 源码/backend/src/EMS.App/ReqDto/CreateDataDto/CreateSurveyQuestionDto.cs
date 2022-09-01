namespace EMS.App.ReqDto.CreateDataDto;
public class CreateSurveyQuestionDto : Base.CreateDataDto
{
    public Guid SurveyId { get; set; }

     public Guid QuestionId { get; set; }

}
