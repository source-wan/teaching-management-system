using System.ComponentModel.DataAnnotations;

namespace EMS.Domain.Base
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <value></value>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// （最后）更新时间
        /// </summary>
        /// <value></value>
        [Required]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 创建人（的Id）
        /// </summary>
        /// <value></value>
        [Column("created_by")]
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// （最后）更新人（的Id）
        /// </summary>
        /// <value></value>
        [Column("updated_by")]
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// 是否启用，默认是
        /// </summary>
        /// <value></value>
        [Column("is_actived")]
        public bool IsActived { get; set; } = true;

        /// <summary>
        /// 是否删除，默认否
        /// </summary>
        /// <value></value>
        [Column("is_deleted")]        
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 备注
        /// </summary>
        /// <value></value>
        [Column("remarks")]
        public string? Remarks { get; set; }
    }
}