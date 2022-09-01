namespace EMS.Domain.Entity;
[Table("teacher_info")]
public class TeacherInfo : BaseEntity
{
    [Column("work_code")]
    public string? WorkCode { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("gender")]
    public bool Gender { get; set; }
    [Column("phone")]
    public string? Phone { get; set; }
    [Column("mail")]
    public string? Mail { get; set; }
    [Required]
    [Column("identity_code")]
    public string IdentityCode { get; set; } = null!;
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Column("academy_id")]
    public Guid AcademyId {get;set;}
}
