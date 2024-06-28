using Electric.API.Auth;
using Electric.API.UOW;
using Electric.Entity;
using Electric.Entity.Accounts;
using Electric.Entity.Auths;
using Electric.Entity.Commons;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Electric.Service.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Electric.API.Controllers
{
    /// <summary>
    /// 账号
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EletricAuthorize]
    public class AccountsController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<EleUser> _userManager;
        private readonly SignInManager<EleUser> _signInManager;

        public AccountsController(IRolePermissionService rolePermissionService, IHttpContextAccessor httpContextAccessor, UserManager<EleUser> userManager, SignInManager<EleUser> signInManager)
        {
            _rolePermissionService = rolePermissionService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UnitOfWork(IsTransactional = false)]
        public async Task<IActionResult> Get()
        {
            //获取个人信息
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            //返回结果
            var accountDto = new AccountDto()
            {
                Roles = roles.ToArray(),
                Name = userName,
                Avatar = "",
                Introduction = string.IsNullOrEmpty(user.FullName) ? userName : user.FullName
            };

            return Ok(accountDto);
        }

        /// <summary>
        /// 获取授权列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("permissions")]
        [UnitOfWork(IsTransactional = false)]
        public async Task<IActionResult> GetPermissions()
        {
            //获取登录的用户名
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var entity = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(entity);

            var rolePermissions = new Dictionary<long, AccountPermissionsDto>();
            foreach (var roleName in roles)
            {
                //获取角色列表
                var _rolePermissions = _rolePermissionService.GetRolePermissions(roleName);
                //合并角色重复的权限
                foreach (var permission in _rolePermissions)
                {
                    if(!rolePermissions.ContainsKey(permission.Id))
                    {
                        rolePermissions.Add(permission.Id, permission);
                    }
                }
            }

            //返回权限列表
            var list = rolePermissions.Values.ToList();
            return Ok(list);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="accountUpdatePassword"></param>
        [HttpPatch("password")]
        public async Task<IActionResult> Put([FromBody] AccountUpdatePasswordDto accountUpdatePassword)
        {
            //获取登录的用户名
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var result = await _signInManager.PasswordSignInAsync(userName, accountUpdatePassword.OldPassword, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);

                //修改密码
                PasswordHasher<EleUser> ph = new PasswordHasher<EleUser>();
                user.PasswordHash = ph.HashPassword(user, accountUpdatePassword.NewPassword);
                await _userManager.UpdateAsync(user);

                return Ok();
            }
            else
            {
                //错误
                var responseResult = new ResponseResultDto();
                responseResult.SetError("原密码错误");
                return BadRequest(responseResult);
            }
        }
    }
}
