namespace EMS.App.ReqDto.UpdateDataDto;
public class UpdateTextBookDto : Base.UpdateDataDto
{
    public string? ISBN { get; set; }
    public string? BookName { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublishAt { get; set; }
    public Decimal? Price { get; set; }
}
