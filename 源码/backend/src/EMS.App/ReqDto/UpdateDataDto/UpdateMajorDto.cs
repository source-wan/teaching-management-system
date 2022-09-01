namespace EMS.App.ReqDto.UpdateDataDto;
public class UpdateMajorDto : Base.UpdateDataDto
{
    public string? MajorName { get; set; }
    public Guid? AcademyId { get; set; }
}
