namespace EMS.Domain.Entity
{
    [Table("textbook_info")]
    public class TextBookInfo : BaseEntity
    {
        [Column("isbn")]
        public string ISBN { get; set; } = null!;

        [Column("book_name")]
        public string BookName { get; set; } = null!;

        [Column("author")]
        public string Author { get; set; } = null!;

        [Column("publisher")]
        public string Publisher { get; set; } = null!;

        [Column("publish_at")]
        public DateTime PublishAt { get; set; }

        [Column("price")][DataType("decimal(3,2)")]
        public Decimal Price { get; set; }
    }
}