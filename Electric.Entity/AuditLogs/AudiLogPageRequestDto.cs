using Electric.Entity.Commons;

namespace Electric.Entity.AuditLogs
{
    /// <summary>
    /// 审核日志翻页查询
    /// </summary>
    public class AudiLogPageRequestDto : PageRequestDto
    {
        /// <summary>
        /// API接口地址
        /// </summary>
        public string? ApiUrl { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string? ClientIpAddress { get; set; }

        /// <summary>
        /// 日志记录区间：开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 日志记录区间：结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 调用用户ID
        /// </summary>
        public long? CreatorId { get; set; }

        /// <summary>
        /// 日志类型 0:正常日志记录，99：异常日志
        /// </summary>
        public List<AuditLogType>? AuditLogTypes { get; set; }
    }
}
