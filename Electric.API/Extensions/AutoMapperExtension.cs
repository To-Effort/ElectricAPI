using AutoMapper;
using Electric.Entity.Commons;

namespace Electric.API.Extensions
{
    /// <summary>
    /// AutoMapper相关扩展
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// AutoMapper相关注入
        /// </summary>
        /// <param name="builder"></param>
        public static void AddElectricAutoMapper(this WebApplicationBuilder builder)
        {
            AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, Mapper>();
        }
    }
}
