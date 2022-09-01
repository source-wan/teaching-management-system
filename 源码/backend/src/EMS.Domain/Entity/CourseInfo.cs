namespace EMS.Domain.Entity
{
    [Table("course_info")]
    public class CourseInfo : BaseEntity
    {
        [Column("course_name")]
        public string CourseName { get; set; } = null!;
        [Column("credit")]
        [DataType("decimal(1,2)")]
        public decimal Credit { get; set; }
        [Column("period")]
        public string Period { get; set; } = null!;
    }
}