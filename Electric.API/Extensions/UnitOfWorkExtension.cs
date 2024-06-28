using Electric.Service.UOW;

namespace Electric.API.Extensions
{
    /// <summary>
    /// 工作单元相关扩展
    /// </summary>
    public static class UnitOfWorkExtension
    {
        /// <summary>
        /// 工作单元注入
        /// </summary>
        /// <param name="builder"></param>
        public static void AddUnitOfWork(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
