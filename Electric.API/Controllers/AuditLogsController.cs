using Electric.API.AuditLog;
using Electric.API.Auth;
using Electric.Entity.AuditLogs;
using Electric.Entity.Commons;
using Electric.Service.AuditLogs;
using Microsoft.AspNetCore.Mvc;

namespace Electric.API.Controllers
{
    /// <summary>
    /// 审核日志
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EletricAuthorize]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="auditLogService"></param>
        public AuditLogsController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        /// <summary>
        /// 根据筛选条件，分页返回审核日志列表
        /// </summary>
        /// <param name="audiLogPageRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [AuditLog(OpenLog = false)]
        public IActionResult Get([FromQuery] AudiLogPageRequestDto audiLogPageRequestDto)
        {
            var auditLogPageResponseDto = _auditLogService.Query(audiLogPageRequestDto);
            return Ok(auditLogPageResponseDto);
        }

        /// <summary>
        /// 删除审核日志
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [AuditLog(OpenLog = false)]
        public IActionResult Delete(long id)
        {
            var successed = _auditLogService.Delete(id);
            if(successed)
            {
                return NoContent();
            }
            else
            {
                var responseResult = new ResponseResultDto();
                responseResult.SetNotFound();
                return BadRequest(responseResult);
            }
        }
    }
}
