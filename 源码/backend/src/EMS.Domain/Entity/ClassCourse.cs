namespace EMS.Domain.Entity
{
    [Table("class_course")]
    public class ClassCourse : BaseEntity
    {
        [Column("class_id")]
        public Guid ClassId { get; set; }
        [Column("course_id")]
        public Guid CourseId { get; set; }
        [Column("teacher_id")]
        public Guid TeacherId { get; set; }
        [Column("book_id")]
        public Guid BookId { get; set; }
        [Column("term")]
        public int Term { get; set; }
    }
}