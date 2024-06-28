using Electric.Entity.Permissions;

namespace Electric.Service.Permissions
{
    public interface IPermissionService
    {
        /// <summary>
        /// 更新权限信息
        /// </summary>
        /// <param name="elePermission"></param>
        /// <returns></returns>
        ElePermission Add(ElePermission elePermission);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        bool Delete(long id);

        // <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ElePermission? Get(long id);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        List<PermissionDto> GetAll();

        /// <summary>
        /// 更新权限信息
        /// </summary>
        /// <param name="elePermission"></param>
        /// <returns></returns>
        bool Update(ElePermission elePermission);
    }
}