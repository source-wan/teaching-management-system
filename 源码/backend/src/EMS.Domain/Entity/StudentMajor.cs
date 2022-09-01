namespace EMS.Domain.Entity
{
    [Table("student_major")]
    public class StudentMajor : BaseEntity
    {
        [Column("student_id")]
        public Guid StudentId { get; set; }
        [Column("major_id")]
        public Guid MajorId { get; set; }
    }
}