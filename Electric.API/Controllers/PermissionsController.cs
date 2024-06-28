using AutoMapper;
using Electric.API.Auth;
using Electric.API.UOW;
using Electric.Entity.Commons;
using Electric.Entity.Permissions;
using Electric.Entity.Roles;
using Electric.Service.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Electric.API.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EletricAuthorize]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public PermissionsController(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [UnitOfWork(IsTransactional = false)]
        public IActionResult All()
        {
            //获取所有权限
            var permissionDtos = _permissionService.GetAll();

            return Ok(permissionDtos);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permissionCreateDto"></param>
        [HttpPost]
        public IActionResult Post([FromBody] PermissionCreateDto permissionCreateDto)
        {
            var elePermission = _mapper.Map<ElePermission>(permissionCreateDto);
            //添加权限
            elePermission = _permissionService.Add(elePermission);

            if (elePermission != null)
            {
                return Created(string.Empty, elePermission.Id);
            }
            else
            {
                var responseResult = new ResponseResultDto();
                responseResult.SetError("请菜单编码，是否重复！");
                return BadRequest(responseResult);
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionUpdateDto"></param>
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] PermissionUpdateDto permissionUpdateDto)
        {
            //获取权限
            var elePermission = _permissionService.Get(id);
            if(elePermission == null)
            {
                var responseResult = new ResponseResultDto();
                responseResult.SetNotFound();
                return BadRequest(responseResult);
            }

            //更新字段
            _mapper.Map(permissionUpdateDto, elePermission);
            var success = _permissionService.Update(elePermission);

            if (success)
            {
                return NoContent();
            }
            else
            {
                var notFound = new ResponseResultDto();
                notFound.SetError("请菜单编码，是否重复！");
                return BadRequest(notFound);
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //删除权限
            var success = _permissionService.Delete(id);
            
            if(success)
            {
                return NoContent();
            }
            else
            {
                var notFound = new ResponseResultDto();
                notFound.SetNotFound();
                return BadRequest(notFound);
            }
        }
    }
}
