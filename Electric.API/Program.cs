using Autofac;
using Autofac.Extensions.DependencyInjection;
using Electric.API.Extensions;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);



//添加速率限制
builder.AddRateLimiter();



//配置host与容器
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModuleRegister());
    });

//开启日志
builder.Logging.AddLog4Net();

// 将服务添加到容器中。
builder.AddElectricControllers();

// Swagger/OpenAPI 服务
builder.AddElectricSwaggerGen();

//数据库上下文注入
builder.AddElectricDbContext();

//添加jwt验证
builder.AddElectricJWT();

//添加注入
builder.AddElectricService();

//AutoMapper依赖注入
builder.AddElectricAutoMapper();

//跨域配置
var eleAllowSpecificOrigins = "EleAllowSpecificOrigins";
builder.AddElectricCorsOrigins(eleAllowSpecificOrigins);

//工作单元注入
builder.AddUnitOfWork();

var app = builder.Build();

//跨域配置
app.UseCors(eleAllowSpecificOrigins);

// 配置HTTP请求管道。
if (app.Environment.IsDevelopment())
{
    //开发环境下，才开启Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//允许body重用
app.Use(next => context =>
{
    context.Request.EnableBuffering();
    return next(context);
});

app.MapControllers();
//API速率限制
app.UseRateLimiter();


app.Run();