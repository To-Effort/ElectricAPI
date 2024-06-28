using Electric.Entity.Commons;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Electric.Entity.AuditLogs
{
    /// <summary>
    /// 审核日志
    /// </summary>
    [Index(nameof(AuditLogType))]
    public class EleAuditLog:EleEntity
    {
        /// <summary>
        /// API接口地址
        /// </summary>
        [MaxLength(256)]
        [Comment("API接口地址")]
        public string ApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// 接口的方法
        /// </summary>
        [MaxLength(256)]
        [Comment("接口的方法")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 参数
        /// </summary>
        [MaxLength(1024)]
        [Comment("参数")]
        public string Parameters { get; set; } = string.Empty;

        /// <summary>
        /// 返回结果
        /// </summary>
        [Comment("返回结果")]
        public string ReturnValue { get; set; } = string.Empty;

        /// <summary>
        /// 执行时间
        /// </summary>
        [Comment("执行时间")]
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        [MaxLength(64)]
        [Comment("客户端IP")]
        public string ClientIpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [MaxLength(512)]
        [Comment("浏览器信息")]
        public string BrowserInfo { get; set; } = string.Empty;

        /// <summary>
        /// 日志类型
        /// </summary>
        [Comment("日志类型 0:正常日志记录，99：异常日志")]
        public AuditLogType AuditLogType { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [MaxLength(1024)]
        [Comment("异常信息")]
        public string ExceptionMessage { get; set; } = string.Empty;

        /// <summary>
        /// 详细异常
        /// </summary>
        [MaxLength(2000)]
        [Comment("详细异常")]
        public string Exception { get; set; } = string.Empty;
    }

    /// <summary>
    /// 审核日志类型
    /// </summary>
    public enum AuditLogType
    {
        /// <summary>
        /// 正常日志记录
        /// </summary>
        Info,

        /// <summary>
        /// 异常日志
        /// </summary>
        Exception = 99
    }
}
