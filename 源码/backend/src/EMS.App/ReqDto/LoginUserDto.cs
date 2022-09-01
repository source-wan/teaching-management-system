namespace EMS.App.ReqDto;
public class LoginUserDto : RequestDto
{
    public string? Account { get; set; }
    public string? Password { get; set; }
    public string? WeChatOpenId { get; set; }
}
