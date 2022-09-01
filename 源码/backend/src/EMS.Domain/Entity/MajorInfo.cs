namespace EMS.Domain.Entity
{
    [Table("major_info")]
    public class MajorInfo : BaseEntity
    {
        [Column("major_name")]
        public string MajorName { get; set; } = null!;

        [Column("major_code")]
        public Int16 MajorCode { get; set; }

        [Column("academy_id")]
        public Guid AcademyId { get; set; }
    }
}