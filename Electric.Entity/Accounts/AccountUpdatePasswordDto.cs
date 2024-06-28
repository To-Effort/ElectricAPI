using Electric.Entity.Commons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electric.Entity.Accounts
{
    /// <summary>
    /// 登录账号密码修改
    /// </summary>
    public class AccountUpdatePasswordDto
    {
        /// <summary>
        /// 原本密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [JsonConverter(typeof(DesensitizationConvter), 2, 4)]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [JsonConverter(typeof(DesensitizationConvter), 2, 4)]
        public string NewPassword { get; set; }
    }
}
