namespace EMS.Domain.Entity
{
    [Table("course_textbook")]
    public class CourseTextBook : Base.BaseEntity
    {
        [Column("course_id")]
        public Guid CourseId { get; set; }
        [Column("textbook_id")]
        public Guid TextBookId { get; set; }
    }
}