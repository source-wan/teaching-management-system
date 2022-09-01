namespace EMS.Domain.Entity
{
    [Table("user_group_user_info")]
    public class UserGroupUserInfo : BaseEntity
    {
        [Column("user_info_id")]
        public Guid UserId { get; set; }
        [Column("user_group_id")]
        public Guid UserGroupId { get; set; }
    }
}