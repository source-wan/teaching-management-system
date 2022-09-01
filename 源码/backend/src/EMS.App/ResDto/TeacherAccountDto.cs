namespace EMS.App.ResDto;

public class TeacherAccountDto
{
    public Guid TeacherId { get; set; }
    public Guid UserId { get; set; }
    public Guid AcademyId {get;set;}
    public string Account { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string IdentityCode { get; set; } = null!;
    public string? WorkCode { get; set; } = null!;
}
