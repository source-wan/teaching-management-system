namespace EMS.Domain.Entity
{
    [Table("identity_user")]
    public class IdentityUser : BaseEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }
        
        [Column("account")]
        public string Account { get; set; } = null!;
        [Column("refreshToken")]
        public string RefreshToken { get; set; } = null!;
        [Column("refreshTokenExpiration")]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}