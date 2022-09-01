namespace EMS.App.ReqDto.CreateDataDto;
public class CreateTeacherDto : Base.CreateDataDto
{
    public string IdentityCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool Gender { get; set; }
    public Guid AcademyId {get;set;}
    Guid CourseId {get;set;}
}
