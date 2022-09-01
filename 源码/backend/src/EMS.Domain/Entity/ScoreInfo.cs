namespace EMS.Domain.Entity
{
    [Table("score_info")]
    public class ScoreInfo : BaseEntity
    {
        [Column("student_id")]
        public Guid StudentId { get; set; }
        [Column("course_id")]
        public Guid CourseId { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("term")]
        public int Term { get; set; }
    }
}