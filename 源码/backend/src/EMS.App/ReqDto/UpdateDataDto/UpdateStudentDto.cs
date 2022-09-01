namespace EMS.App.ReqDto.UpdateDataDto;
public class UpdateStudentDto : Base.UpdateDataDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? IdentityCode { get; set; }
    public string? Phone { get; set; }
    public string? Mail { get; set; }
    public string? QQ { get; set; }
    public string? WeChat { get; set; }
    public string? FatherName { get; set; }
    public string? FatherPhone { get; set; }
    public string? MotherName { get; set; }
    public string? MontherPhone { get; set; }
    public string? DormitoryNo { get; set; }
    public Guid? AcademyId { get; set; }
    public Guid? MajorId { get; set; }
    public Guid? ClassId { get; set; }
}
