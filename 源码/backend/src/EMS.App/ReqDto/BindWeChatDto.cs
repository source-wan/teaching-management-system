namespace EMS.App.ReqDto;
public class BindWeChatDto : RequestDto
{
    public string StudentCode { get; set; }=null!;
    public string WeChatOpenId { get; set; } = null!;
}
