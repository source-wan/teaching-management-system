namespace EMS.App.ReqDto.CreateDataDto;
public class CreateSurveyDto : Base.CreateDataDto
{
    public string Name { get; set; }=null!;

    public Guid UserId {get;set;}

    public int PublishAt {get;set;}

    public int ExpireTime { get; set; }
}
