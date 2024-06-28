using Electric.Entity.AuditLogs;
using Electric.Entity.Users;
using Electric.Repository;
using Electric.Service.Commons;
using Microsoft.Extensions.Logging;

namespace Electric.Service.AuditLogs
{
    /// <summary>
    /// 审核日志
    /// </summary>
    public class AuditLogService: BaseService, IAuditLogService
    {
        private readonly IBaseRepository<EleAuditLog> _auditLogRepository;
        private readonly ILogger<AuditLogService> _logger;
        private readonly IBaseRepository<EleUser> _userRepository;

        public AuditLogService(IBaseRepository<EleAuditLog> auditLogRepository, ILogger<AuditLogService> logger, IBaseRepository<EleUser> userRepository)
        {
            _auditLogRepository = auditLogRepository;
            _logger = logger;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 插入审核日志
        /// </summary>
        /// <param name="eleAuditLog"></param>
        /// <returns></returns>
        public EleAuditLog Add(EleAuditLog eleAuditLog)
        {
            try
            {
                return _auditLogRepository.Add(eleAuditLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存日志至数据库异常，接口：{0}\r\nMethod：{1}\r\n参数：{2}\r\nIP：{3}\r\n花费时长：{4}",
                    eleAuditLog.ApiUrl, eleAuditLog.Method, eleAuditLog.Parameters, eleAuditLog.ClientIpAddress, eleAuditLog.ExecutionDuration);
                return eleAuditLog;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            return _auditLogRepository.Delete(id) > 0;
        }

        /// <summary>
        /// 搜索审核日志
        /// </summary>
        /// <param name="audiLogPageRequestDto"></param>
        /// <returns></returns>
        public AuditLogPageResponseDto Query(AudiLogPageRequestDto audiLogPageRequestDto)
        {
            var result = new AuditLogPageResponseDto();
            //过滤
            var audiLogQuery = _auditLogRepository.GetQueryable();
            if (!string.IsNullOrEmpty(audiLogPageRequestDto.ApiUrl))
            {
                audiLogQuery = audiLogQuery.Where(x => x.ApiUrl.Contains(audiLogPageRequestDto.ApiUrl));
            }
            if (!string.IsNullOrEmpty(audiLogPageRequestDto.ClientIpAddress))
            {
                audiLogQuery = audiLogQuery.Where(x => x.ClientIpAddress.Equals(audiLogPageRequestDto.ClientIpAddress));
            }
            if (audiLogPageRequestDto.StartTime != null)
            {
                audiLogQuery = audiLogQuery.Where(x => x.CreationTime >= audiLogPageRequestDto.StartTime);
            }
            if (audiLogPageRequestDto.EndTime != null)
            {
                audiLogQuery = audiLogQuery.Where(x => x.CreationTime <= audiLogPageRequestDto.EndTime);
            }
            if (audiLogPageRequestDto.CreatorId != null)
            {
                audiLogQuery = audiLogQuery.Where(x => x.CreatorId == audiLogPageRequestDto.CreatorId);
            }
            if (audiLogPageRequestDto.AuditLogTypes != null)
            {
                audiLogQuery = audiLogQuery.Where(x => audiLogPageRequestDto.AuditLogTypes.Contains(x.AuditLogType));
            }

            //关联查询
            var complexQuery = (from log in audiLogQuery
                                join u in _userRepository.GetQueryable() on log.CreatorId equals u.Id into temp
                                from utemp in temp.DefaultIfEmpty()
                                select new AuditLogDto
                                {
                                    ApiUrl = log.ApiUrl,
                                    AuditLogType = log.AuditLogType,
                                    BrowserInfo = log.BrowserInfo,
                                    ClientIpAddress = log.ClientIpAddress,
                                    CreationTime = log.CreationTime,
                                    CreatorUserName = utemp.UserName,
                                    Exception = log.Exception,
                                    ExceptionMessage = log.ExceptionMessage,
                                    ExecutionDuration = log.ExecutionDuration,
                                    Id = log.Id,
                                    Method = log.Method,
                                    Parameters = log.Parameters
                                });

            //获取总数
            var count = audiLogQuery.Count();

            //获取列表
            audiLogPageRequestDto.Page = audiLogPageRequestDto.Page < 1 ? 1 : audiLogPageRequestDto.Page;
            var skip = (audiLogPageRequestDto.Page - 1) * audiLogPageRequestDto.PrePage;
            var list = complexQuery.OrderByDescending(x=>x.Id).Skip(skip).Take(audiLogPageRequestDto.PrePage).ToList();


            //返回结果
            result.AuditLogs = list;
            result.Page = audiLogPageRequestDto.Page;
            result.PrePage = audiLogPageRequestDto.PrePage;
            result.Total = count;

            return result;
        }
    }
}
