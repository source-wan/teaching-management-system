namespace EMS.Domain.Entity
{
    [Table("academy_info")]
    public class AcademyInfo : BaseEntity
    {
        [Column("academy_name")]
        public string AcademyName { get; set; } = null!;

        [Column("academy_code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 AcademyCode { get; set; }

        [Column("campus_id")]
        public Guid CampusId { get; set; }
        
    }
}