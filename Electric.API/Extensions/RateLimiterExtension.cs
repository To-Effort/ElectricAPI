using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Electric.API.Extensions
{
    /// <summary>
    /// 速率限制扩展
    /// </summary>
    public static class RateLimiterExtension
    {
        /// <summary>
        /// 添加速率限制配置
        /// </summary>
        /// <param name="builder"></param>
        public static void AddRateLimiter(this WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(_ =>
                _.AddFixedWindowLimiter(policyName: RateLimiterPolicyName.LoginPolicy, options =>
                {
                    options.PermitLimit = 1;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 1;
                }
            ));
        }
    }
}