namespace EMS.Domain.Entity
{
    [Table("user_group_info")]
    public class UserGroupInfo : BaseEntity
    {
        [Column("user_group_name")]
        public string UserGroupName { get; set; } = null!;
        [Column("limit")]
        public int limit { get; set; } = 0;
        [Column("role_id")]
        public Guid RoleId { get; set; }
    }
}