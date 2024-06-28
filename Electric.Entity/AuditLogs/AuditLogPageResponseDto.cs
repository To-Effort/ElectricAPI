using Electric.Entity.Commons;

namespace Electric.Entity.AuditLogs
{
    /// <summary>
    /// 审核日志翻页响应对象
    /// </summary>
    public class AuditLogPageResponseDto : PageResponseDto
    {
        /// <summary>
        /// 审核日志列表
        /// </summary>
        public List<AuditLogDto>? AuditLogs { get; set; }
    }
}
