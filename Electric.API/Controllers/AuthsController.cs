using Electric.API.Auth;
using Electric.API.Extensions;
using Electric.Entity;
using Electric.Entity.Auths;
using Electric.Entity.Commons;
using Electric.Entity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Electric.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthsController : ControllerBase
    {
        private readonly SignInManager<EleUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthsController(SignInManager<EleUser> signInManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="authLoginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting(RateLimiterPolicyName.LoginPolicy)]
        public async Task<IActionResult> Login([FromBody] AuthLoginDto  authLoginDto)
        {
            //根据用户名、密码校验
            var result = await _signInManager.PasswordSignInAsync(authLoginDto.UserName, authLoginDto.Password, false, false);

            if (result.Succeeded)
            {
                //定义JWT的Payload部分
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, authLoginDto.UserName)
                };


                //生成token
                var jwtBearer = _configuration.GetSection(AppSettings.Authentication).GetSection(AppSettings.JwtBearer);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearer.GetValue<string>(AppSettings.SecurityKey)));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var securityToken = new JwtSecurityToken(
                    issuer: jwtBearer.GetValue<string>(AppSettings.Issuer),
                    audience: jwtBearer.GetValue<string>(AppSettings.Audience),
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                //返回token
                return Ok(token);
            }
            else
            {
                //错误
                var responseResult = new ResponseResultDto();
                responseResult.SetError("账号或密码错误");
                return BadRequest(responseResult);
            }
        }
    }
}
