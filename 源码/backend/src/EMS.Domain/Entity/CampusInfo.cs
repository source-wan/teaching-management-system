namespace EMS.Domain.Entity
{
    [Table("campus_info")]
    public class CampusInfo : BaseEntity
    {
        [Column("campus_name")]
        public string CampusName { get; set; } = null!;
        [Column("address")][DataType("text")]
        public string Address { get; set; } = null!;
    }
}