namespace EMS.Domain.Entity
{
    [Table("app_exception_log")]
    public class AppException : BaseEntity
    {
        [Column("short_message")]
        public string ShortMessage { get; set; } = null!;
        [Column("full_message")][DataType("text")]
        public string FullMessage { get; set; } = null!;
    }

}