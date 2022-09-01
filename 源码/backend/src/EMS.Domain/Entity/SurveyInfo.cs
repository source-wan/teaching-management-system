namespace EMS.Domain.Entity
{
    [Table("survey_info")]
    public class SurveyInfo : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("publish_at")]
        public int PublishAt { get; set; }
        [Column("expire_time")]
        public int ExpireTime {get;set;}
    }
}