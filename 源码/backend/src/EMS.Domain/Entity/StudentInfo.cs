namespace EMS.Domain.Entity;
[Table("student_info")]
public class StudentInfo : BaseEntity
{
    [Column("student_code")]
    public string? StudentCode { get; set; }
    [Column("name")]
    public string Name { get; set; } = null!;
    [Column("gender")]
    public bool Gender { get; set; }
    [Column("address")]
    [DataType("text")]
    public string? Address { get; set; }
    [Column("identity_code")]
    public string IdentityCode { get; set; } = null!;
    [Column("phone")]
    public string? Phone { get; set; }
    [Column("mail")]
    public string? Mail { get; set; }
    [Column("qq")]
    public string? QQ { get; set; }
    [Column("wechat")]
    public string? WeChat { get; set; }
    [Column("father_name")]
    public string? FatherName { get; set; }
    [Column("father_phone")]
    public string? FatherPhone { get; set; }
    [Column("monther_name")]
    public string? MotherName { get; set; }
    [Column("monther_phone")]
    public string? MontherPhone { get; set; }
    [Column("dormitory_no")]
    public string? DormitoryNo { get; set; }
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("major_id")]
    public Guid MajorId { get; set; }
}
