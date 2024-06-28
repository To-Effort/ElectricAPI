using Electric.Entity.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Electric.Entity.Roles
{
    public class EleRolePermission : EleEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public long PermissionId { get; set; }
    }
}
