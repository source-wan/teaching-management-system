namespace EMS.App.ReqDto.UpdateDataDto;
public class UpdateAcademyDto : Base.UpdateDataDto
{
    public string? AcademyName { get; set; }
    public Guid? CampusId { get; set; }
}
