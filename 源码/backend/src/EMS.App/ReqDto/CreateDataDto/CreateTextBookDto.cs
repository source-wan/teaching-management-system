namespace EMS.App.ReqDto.CreateDataDto;
public class CreateTextBookDto : Base.CreateDataDto
{
    public string ISBN { get; set; } = null!;
    public string BookName { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Publisher { get; set; } = null!;
    public DateTime PublishAt { get; set; }
    public Decimal Price { get; set; }
}
