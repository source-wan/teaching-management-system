namespace EMS.App.ReqDto;
public class RegUserDto
{
    public string Account { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid UserGroupId { get; set; }
}
