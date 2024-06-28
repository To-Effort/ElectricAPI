using AutoMapper;
using Electric.Entity.Commons;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Electric.Service.Users;
using Electric.Service.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Electric.API.Auth;
using Electric.Service.UOW;
using Electric.API.UOW;
using Electric.API.Exception;
using Electric.API.AuditLog;

namespace Electric.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EletricAuthorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<EleUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor, UserManager<EleUser> userManager, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据用户名搜索，分页返回用户列表
        /// </summary>
        /// <param name="userPageRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [UnitOfWork(IsTransactional = false)]
        public IActionResult Get([FromQuery] UserPageRequestDto userPageRequestDto)
        {

            //根据用户名搜索，分页返回用户列表
            var UserPageResponseDto = _userService.Query(userPageRequestDto);

            return Ok(UserPageResponseDto);
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [AuditLog(OpenLog = false)]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userCreateDto"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateDto userCreateDto)
        {
            var eleUser = _mapper.Map<EleUser>(userCreateDto);

            //获取登录的用户
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var creatorUser = await _userManager.FindByNameAsync(userName);

            //更新字段
            eleUser.CreatorId = creatorUser.Id;
            eleUser.EmailConfirmed = true;
            eleUser.NormalizedUserName = eleUser.UserName;
            eleUser.SecurityStamp = DateTime.Now.Ticks.ToString();

            //密码
            PasswordHasher<EleUser> ph = new PasswordHasher<EleUser>();
            eleUser.PasswordHash = ph.HashPassword(eleUser, userCreateDto.Password);

            //添加用户
            var result = await _userManager.CreateAsync(eleUser);
            if (result.Succeeded)
            {
                return Created(string.Empty, eleUser);
            }
            else
            {
                var responseResult = new ResponseResultDto();
                responseResult.SetError("请检查用户账号，是否重复！");
                return BadRequest(responseResult);
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userUpdateDto"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UserUpdateDto userUpdateDto)
        {
            //获取用户
            var eleUser = await _userManager.FindByIdAsync(id.ToString());
            if (eleUser == null)
            {
                var responseResult = new ResponseResultDto();
                responseResult.SetNotFound();
                return BadRequest(responseResult);
            }

            //更新密码
            if (userUpdateDto.Password == null)
            {
                userUpdateDto.Password = eleUser.PasswordHash;
            }
            else
            {
                PasswordHasher<EleUser> ph = new PasswordHasher<EleUser>();
                eleUser.PasswordHash = ph.HashPassword(eleUser, userUpdateDto.Password);
            }

            //更新用户字段
            _mapper.Map(userUpdateDto, eleUser);
            eleUser.LastModificationTime = DateTime.Now;
            var result = await _userManager.UpdateAsync(eleUser);
            if (!result.Succeeded)
            {
                var notFound = new ResponseResultDto();
                notFound.SetError("请检查用户账号，是否重复！");
                return BadRequest(notFound);
            }

            //删除原本角色
            var oldRoles = await _userManager.GetRolesAsync(eleUser);
            if (oldRoles != null && oldRoles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(eleUser, oldRoles);
            }

            //设置新的角色列表
            result = await _userManager.AddToRolesAsync(eleUser, userUpdateDto.RoleNames);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                var notFound = new ResponseResultDto();
                notFound.SetError();
                return BadRequest(notFound);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var responseResult = new ResponseResultDto();

            var eleUser = await _userManager.FindByIdAsync(id.ToString());
            //初始化数据，不可删除
            if (id == 1)
            {
                responseResult.SetError("初始的用户，不可删除");
                return BadRequest(responseResult);
            }
            else if (eleUser == null)
            {
                responseResult.SetNotFound();
                return BadRequest(responseResult);
            }

            //删除用户
            await _userManager.DeleteAsync(eleUser);
            return NoContent();
        }
    }
}
