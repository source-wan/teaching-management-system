namespace EMS.Domain.Entity
{
    [Table("user_info")]
    public class UserInfo : BaseEntity
    {
        [Column("account")]
        public string Account { get; set; } = null!;
        [Column("password")]
        public string Password { get; set; } = null!;
        [Column("wechat_open_id")]
        public string? WeChatOpenId { get; set; }
    }
}