using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Electric.API.Extensions
{
    /// <summary>
    /// SwaggerGen相关扩展
    /// </summary>
    public static class SwaggerGenExtension
    {
        /// <summary>
        /// Swagger/OpenAPI自定义扩展
        /// </summary>
        /// <param name="builder"></param>
        public static void AddElectricSwaggerGen(this WebApplicationBuilder builder)
        {
            //开启最小Web API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                //开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //在Heder中添加Token 传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}(注意两者之间有一个空格)",
                    Name = "Authorization",//jwt默认的参数名称,
                    In = ParameterLocation.Header,//jwt默认存放Autorization信息的位置（header中）
                    Type = SecuritySchemeType.ApiKey
                });

                // 为 Swagger 设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                c.IncludeXmlComments(xmlPath, true);
            });
        }
    }
}
