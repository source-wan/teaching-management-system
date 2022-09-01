namespace EMS.Domain.Entity
{
    public enum RoleType
    {
        Admin = 0,
        Teacher,
        Instructor,
        Student
    }
    [Table("role_info")]
    public class RoleInfo : BaseEntity
    {
        [Required]
        [Column("role_name")]
        public RoleType RoleName { get; set; }
    }

}