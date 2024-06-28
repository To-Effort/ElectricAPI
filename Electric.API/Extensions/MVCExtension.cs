using Electric.API.AuditLog;
using Electric.API.Exception;
using Electric.API.UOW;

namespace Electric.API.Extensions
{
    /// <summary>
    /// MVC相关扩展
    /// </summary>
    public static class MVCExtension
    {
        /// <summary>
        /// 控制器自定义返回格式
        /// </summary>
        /// <param name="builder"></param>
        public static void AddElectricControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(configure => {
                configure.Filters.Add<UnitOfWorkFilterAttribute>();
                configure.Filters.Add<ElectricExceptionFilterAttribute>();
                configure.Filters.Add<EletricActionFilterAttribute>();
            })
            .AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }
    }
}
