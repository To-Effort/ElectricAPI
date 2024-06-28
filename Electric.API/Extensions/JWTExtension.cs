using Electric.API.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Electric.API.Extensions
{
    /// <summary>
    /// JWT相关扩展
    /// </summary>
    public static class JWTExtension
    {
        /// <summary>
        /// JWT、认证相关
        /// </summary>
        /// <param name="builder"></param>
        public static void AddElectricJWT(this WebApplicationBuilder builder)
        {
            var jwtBearer = builder.Configuration.GetSection(AppSettings.Authentication).GetSection(AppSettings.JwtBearer);
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidIssuer = jwtBearer.GetValue<string>(AppSettings.Issuer),//Issuer

                    ValidateAudience = true,//是否验证Audience
                    ValidAudience = jwtBearer.GetValue<string>(AppSettings.Audience),//Audience

                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearer.GetValue<string>(AppSettings.SecurityKey))),//拿到SecurityKey

                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(5)//偏差秒数：防止客户端与服务器时间偏差
                };
            });
        }
    }
}
