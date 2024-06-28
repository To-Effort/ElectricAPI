using Electric.Entity.Permissions;
using Electric.Entity.Users;
using Electric.Service.Commons;

namespace Electric.API.Extensions
{
    /// <summary>
    /// Service相关扩展
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Service注入
        /// </summary>
        /// <param name="builder"></param>
        public static void AddElectricService(this WebApplicationBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetReferanceAssemblies();
            foreach (var assembly in assemblies)
            {
                //获取继承BaseService的类
                List<Type> types = assembly.GetTypes()
                    .Where(t => t.BaseType == typeof(BaseService))
                    .ToList();

                types.ForEach(impl =>
                {
                    //获取该类所继承的所有接口
                    Type[] interfaces = impl.GetInterfaces();
                    interfaces.ToList().ForEach(i =>
                    {
                        builder.Services.AddTransient(i, impl);
                    });
                });
            }
        }
    }
}
