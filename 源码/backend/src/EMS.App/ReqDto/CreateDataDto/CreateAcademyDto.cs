namespace EMS.App.ReqDto.CreateDataDto;
public class CreateAcademyDto : Base.CreateDataDto
{
    public Guid CampusId { get; set; }
    public string AcademyName { get; set; } = null!;
}
