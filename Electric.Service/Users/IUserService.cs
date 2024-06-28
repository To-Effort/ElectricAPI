using Electric.Entity.Users;

namespace Electric.Service.Users
{
    public interface IUserService
    {
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns></returns>
        List<UserAllDto> GetAll();

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<string> GetRoles(string userName);

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="userPageRequestDto"></param>
        /// <returns></returns>
        UserPageResponseDto Query(UserPageRequestDto userPageRequestDto);
    }
}