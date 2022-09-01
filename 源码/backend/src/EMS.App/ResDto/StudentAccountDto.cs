namespace EMS.App.ResDto;

public class StudentAccountDto
{
    public Guid StudentId { get; set; }
    public Guid UserId { get; set; }
    public string Account { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string StudentName { get; set; } = null!;
    public string IdentityCode { get; set; } = null!;
    public string StudentCode { get; set; } = null!;
}
