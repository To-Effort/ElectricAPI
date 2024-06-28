using Electric.Entity.Commons;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electric.Entity.Auths
{
    public class AuthLoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [JsonConverter(typeof(DesensitizationConvter), 2, 4)]
        public string Password { get; set; }
    }
}
