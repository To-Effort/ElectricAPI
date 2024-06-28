using Electric.Repository;
using Autofac;

namespace Electric.API.Extensions
{
    /// <summary>
    /// 模块注册
    /// </summary>
    public  class AutofacModuleRegister :Module
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency(); //注册仓储
        }
    }
}
