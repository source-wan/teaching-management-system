namespace EMS.App.ReqDto.CreateDataDto;
public class CreateMajorDto : Base.CreateDataDto
{
    public string MajorName { get; set; } = null!;
    public Guid AcademyId { get; set; }
}
