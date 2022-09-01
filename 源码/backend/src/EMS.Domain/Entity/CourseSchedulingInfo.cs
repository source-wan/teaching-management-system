namespace EMS.Domain.Entity
{
    [Table("course_scheduling_info")]
    public class CourseSchedulingInfo : BaseEntity
    {
        [Column("class_id")]
        public Guid ClassId { get; set; }
        [Column("teacher_id")]
        public Guid TeacherId { get; set; }

        [Column("address")][DataType("text")]
        public string Address { get; set; } = null!;
        [Column("class_time")]
        public DateTime ClassTime { get; set; }
    }
}