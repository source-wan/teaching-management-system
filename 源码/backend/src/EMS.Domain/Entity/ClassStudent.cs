namespace EMS.Domain.Entity
{
    [Table("class_student")]
    public class ClassStudent : BaseEntity
    {
        [Column("class_id")]
        public Guid ClassId { get; set; }
        [Column("student_id")]
        public Guid StudentId { get; set; }
        [Column("join_time")]
        public DateTime JoinTime { get; set; }
    }
}