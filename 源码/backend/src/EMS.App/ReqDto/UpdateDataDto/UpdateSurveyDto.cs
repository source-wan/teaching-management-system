namespace EMS.App.ReqDto.UpdateDataDto;
public class UpdateSurveyDto : Base.UpdateDataDto
{
    public string? Name { get; set; }

    public int? PublishAt { get; set; }

    public int? ExpireTime { get; set; }

}
