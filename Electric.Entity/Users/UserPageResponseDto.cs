using Electric.Entity.Commons;

namespace Electric.Entity.Users
{
    /// <summary>
    /// 用户翻页响应对象
    /// </summary>
    public class UserPageResponseDto: PageResponseDto
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UserDto> Users { get; set; }
    }
}
