using Electric.API.Auth;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Electric.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Electric.API.Extensions
{
    /// <summary>
    /// 数据库相关扩展
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// 数据库上下文注入
        /// </summary>
        /// <param name="builder"></param>
        public static void AddElectricDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //启用的数据库类型
                var provider = builder.Configuration.GetValue<string>(AppSettings.DataProvider);
                switch (provider)
                {
                    case "MsSql":
                        var msSqlConnection = builder.Configuration.GetConnectionString(AppSettings.MsSqlConnection) ?? throw new InvalidOperationException("MsSqlConnection在appsettings.json未发现");
                        options.UseSqlServer(msSqlConnection);
                        break;
                    case "MySql":
                        var mySqlConnection = builder.Configuration.GetConnectionString(AppSettings.MySqlConnection) ?? throw new InvalidOperationException("MySqlConnection在appsettings.json未发现");
                        //MySql需要传入版本，ServerVersion.AutoDetect根据连接字符串自动获取
                        options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection));
                        break;
                }
                options.LogTo(Console.WriteLine); //打印SQL日志，方便调试
            });

            //Identity注入，添加数据库上下文
            builder.Services.AddIdentity<EleUser, EleRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }
    }
}
