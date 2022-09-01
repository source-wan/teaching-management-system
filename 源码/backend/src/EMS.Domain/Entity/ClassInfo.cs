namespace EMS.Domain.Entity
{
    [Table("class_info")]
    public class ClassInfo : BaseEntity
    {
        [Column("class_name")]
        public string ClassName { get; set; } = null!;

        [Column("class_code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 ClassCode { get; set; }
        [Column("Instructor_id")]
        public Guid InstroctorId { get; set; }
        [Column("major_id")]
        public Guid MajorId { get; set; }

    }
}