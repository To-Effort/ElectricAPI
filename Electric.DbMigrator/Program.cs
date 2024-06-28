using Electric.EntityFrameworkCore;
using Electric.Entity;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//数据库上下文注入
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //启用的数据库类型
    var provider = builder.Configuration.GetValue<string>("DataProvider");
    switch (provider)
    {
        case "MySql":
            var mySqlConnection = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("MySqlConnection在appsettings.json未发现");
            //MySql需要传入版本，ServerVersion.AutoDetect根据连接字符串自动获取
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection), b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            break;
        case "MsSql":
            var msSqlConnection = builder.Configuration.GetConnectionString("MsSqlConnection") ?? throw new InvalidOperationException("MsSqlConnection在appsettings.json未发现");
            options.UseSqlServer(msSqlConnection, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            break;
        
    }
});


//Identity注入，添加数据库上下文
builder.Services.AddIdentity<EleUser, EleRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var app = builder.Build();
app.Run();