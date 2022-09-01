namespace EMS.App.ReqDto.CreateDataDto;
public class CreateCampusDto : Base.CreateDataDto
{
    public string CampusName { get; set; } = null!;
    public string Address { get; set; } = null!;
}
