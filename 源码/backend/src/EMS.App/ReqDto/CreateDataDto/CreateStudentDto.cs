namespace EMS.App.ReqDto.CreateDataDto;
public class CreateStudentDto : Base.CreateDataDto
{
    public string StudentName { get; set; } = null!;
    public string IdentityCode { get; set; } = null!;
    public bool Gender { get; set; }
    // public Guid AcademyId { get; set; }
    public Guid MajorId { get; set; }
}
