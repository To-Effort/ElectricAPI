using AutoMapper;
using Electric.API.Auth;
using Electric.API.UOW;
using Electric.Entity.Commons;
using Electric.Entity.Permissions;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Electric.Service.Permissions;
using Electric.Service.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Electric.API.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EletricAuthorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<EleUser> _userManager;
        private readonly RoleManager<EleRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IRolePermissionService rolePermissionService, IHttpContextAccessor httpContextAccessor, 
            UserManager<EleUser> userManager, IMapper mapper, RoleManager<EleRole> roleManager)
        {
            _roleService = roleService;
            _rolePermissionService = rolePermissionService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 角色搜索
        /// </summary>
        /// <param name="rolePageRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [UnitOfWork(IsTransactional = false)]
        public IActionResult Get([FromQuery] RolePageRequestDto rolePageRequestDto)
        {
            //角色搜索
            var rolePageResponseDto = _roleService.Query(rolePageRequestDto);

            return Ok(rolePageResponseDto);
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [UnitOfWork(IsTransactional = false)]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();

            return Ok(roles);
        }

        /// <summary>
        /// 保存角色的权限列表
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <param name="rolePermissionDto">以,分割权限Id</param>
        /// <returns></returns>
        [HttpPut("{id}/permissions")]
        public async Task<IActionResult> SavePermissions(long id, [FromBody] RolePermissionDto rolePermissionDto)
        {
            //获取个人信息
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            //保存权限
            _rolePermissionService.SavePermissions(id, rolePermissionDto.PermissionIds, user.Id);

            return Ok();
        }

        /// <summary>
        /// 获取角色的权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/permissions")]
        [UnitOfWork(IsTransactional = false)]
        public IActionResult GetPermissions(long id)
        {
            //获取角色的权限列表
            var rolePermissionDtos = _rolePermissionService.GetRolePermissions(id);

            return Ok(rolePermissionDtos);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleCreateDto"></param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleCreateDto roleCreateDto)
        {
            var eleRole = _mapper.Map<EleRole>(roleCreateDto);

            //获取登录的用户
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var creatorUser = await _userManager.FindByNameAsync(userName);

            //添加角色
            eleRole.CreatorId = creatorUser.Id;
            var result = await _roleManager.CreateAsync(eleRole);
            if(result.Succeeded)
            {
                return Created(string.Empty, eleRole);
            }
            else
            {
                var responseResult = new ResponseResultDto();
                responseResult.SetError("请检查角色名称，是否重复！");
                return BadRequest(responseResult);
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleUpdateDto"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] RoleUpdateDto roleUpdateDto)
        {
            //获取角色
            var eleRole = await _roleManager.FindByIdAsync(id.ToString());
            if (eleRole == null)
            {
                var responseResult = new ResponseResultDto();
                responseResult.SetNotFound();
                return BadRequest(responseResult);
            }

            //更新字段
            _mapper.Map(roleUpdateDto, eleRole);
            eleRole.LastModificationTime = DateTime.Now;
            var result = await _roleManager.UpdateAsync(eleRole);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                var notFound = new ResponseResultDto();
                notFound.SetError("请检查角色名称，是否重复！");
                return BadRequest(notFound);
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var responseResult = new ResponseResultDto();

            var eleRole = await _roleManager.FindByIdAsync(id.ToString());
            //初始化数据，不可删除
            if (id == 1)
            {
                responseResult.SetError("初始的角色，不可删除");
                return BadRequest(responseResult);
            }
            else if (eleRole == null)
            {
                responseResult.SetNotFound();
                return BadRequest(responseResult);
            }

            //删除角色
            await _roleManager.DeleteAsync(eleRole);
            return NoContent();
        }
    }
}
