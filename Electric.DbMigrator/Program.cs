using Electric.EntityFrameworkCore;
using Electric.Entity;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//���ݿ�������ע��
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //���õ����ݿ�����
    var provider = builder.Configuration.GetValue<string>("DataProvider");
    switch (provider)
    {
        case "MySql":
            var mySqlConnection = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("MySqlConnection��appsettings.jsonδ����");
            //MySql��Ҫ����汾��ServerVersion.AutoDetect���������ַ����Զ���ȡ
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection), b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            break;
        case "MsSql":
            var msSqlConnection = builder.Configuration.GetConnectionString("MsSqlConnection") ?? throw new InvalidOperationException("MsSqlConnection��appsettings.jsonδ����");
            options.UseSqlServer(msSqlConnection, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            break;
        
    }
});


//Identityע�룬������ݿ�������
builder.Services.AddIdentity<EleUser, EleRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var app = builder.Build();
app.Run();