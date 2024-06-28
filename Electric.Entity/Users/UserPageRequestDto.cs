using Electric.Entity.Commons;

namespace Electric.Entity.Users
{
    /// <summary>
    /// 用户翻页查询
    /// </summary>
    public class UserPageRequestDto : PageRequestDto
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string? Name { get; set; }
    }
}
