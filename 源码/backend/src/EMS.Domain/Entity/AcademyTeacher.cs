namespace EMS.Domain.Entity
{
    [Table("academy_teacher")]
    public class AcademyTeacher : BaseEntity
    {
        [Column("academy_id")]
        public Guid Academy_id { get; set; }
        [Column("teacher_id")]
        public Guid Teacher_id { get; set; }
    }
}