namespace EMS.App.ReqDto.UpdateDataDto;
public class UpdateTeacherDto : Base.UpdateDataDto
{
    public string? WorkCode { get; set; }

    public string? Name { get; set; }

    public bool? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public string? IdentityCode { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ClassId { get; set; }
    
}
