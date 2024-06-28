using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electric.Entity.Accounts
{
    /// <summary>
    /// 登录账号信息
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduction { get; set; }
    }
}
