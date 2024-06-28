using Electric.Entity.AuditLogs;

namespace Electric.Service.AuditLogs
{
    public interface IAuditLogService
    {
        /// <summary>
        /// 插入审核日志
        /// </summary>
        /// <param name="eleAuditLog"></param>
        /// <returns></returns>
        EleAuditLog Add(EleAuditLog eleAuditLog);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(long id);

        /// <summary>
        /// 搜索审核日志
        /// </summary>
        /// <param name="audiLogPageRequestDto"></param>
        /// <returns></returns>
        AuditLogPageResponseDto Query(AudiLogPageRequestDto audiLogPageRequestDto);
    }
}
