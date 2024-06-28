using Electric.Entity.Commons;
using Newtonsoft.Json;

namespace Electric.Entity.AuditLogs
{
    /// <summary>
    /// 审核日志信息
    /// </summary>
    public class AuditLogDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 调用用户名称
        /// </summary>
        public string? CreatorUserName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? ApiUrl { get; set; }

        /// <summary>
        /// 接口的方法
        /// </summary>
        public string? Method { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [JsonConverter(typeof(DesensitizationConvter), 4, 4)]
        public string? ClientIpAddress { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string? BrowserInfo { get; set; }

        /// <summary>
        /// 日志类型 0:正常日志记录，99：异常日志
        /// </summary>
        public AuditLogType AuditLogType { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string? ExceptionMessage { get; set; }

        /// <summary>
        /// 详细异常
        /// </summary>
        public string? Exception { get; set; }
    }
}
