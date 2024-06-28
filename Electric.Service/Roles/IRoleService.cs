using Electric.Entity.Roles;

namespace Electric.Service.Roles
{
    public interface IRoleService
    {
        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <param name="rolePageRequestDto"></param>
        /// <returns></returns>
        List<RoleDto> GetAll();

        /// <summary>
        /// 搜索角色
        /// </summary>
        /// <param name="rolePageRequestDto"></param>
        /// <returns></returns>
        RolePageResponseDto Query(RolePageRequestDto rolePageRequestDto);
    }
}