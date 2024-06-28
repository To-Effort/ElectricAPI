using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Electric.Entity.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    public class EleUser : IdentityUser<long>
    {
        [Comment("创建者Id")]
        public long CreatorId { get; set; }

        [Comment("创建时间")]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        [Comment("最后编辑时间")]
        public DateTime LastModificationTime { get; set; }

        /// <summary>
        /// 全名：姓名
        /// </summary>
        [MaxLength(20)]
        public string? FullName { get; set; }

        /// <summary>
        /// 状态，0：禁用，1：正常
        /// </summary>
        public UserStatus Status { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        /// 已禁用
        /// </summary>
        Forbidden = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1
    }
}
