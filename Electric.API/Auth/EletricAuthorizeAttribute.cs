using Electric.API.AuditLog;
using Electric.Entity.Accounts;
using Electric.Entity.AuditLogs;
using Electric.Entity.Users;
using Electric.Service.AuditLogs;
using Electric.Service.Permissions;
using Electric.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Electric.API.Auth
{
    /// <summary>
    /// API权限验证
    /// </summary>
    public class EletricAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Identity.Name;

            var _userService = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
            var _rolePermissionService = context.HttpContext.RequestServices.GetService(typeof(IRolePermissionService)) as IRolePermissionService;
            var _permissionService = context.HttpContext.RequestServices.GetService(typeof(IPermissionService)) as IPermissionService;
            var _auditLogService = context.HttpContext.RequestServices.GetService(typeof(IAuditLogService)) as IAuditLogService;
            var _userManager = context.HttpContext.RequestServices.GetService(typeof(UserManager<EleUser>)) as UserManager<EleUser>;

            //判断此Api在权限列表是否有配置，如果未配置，默认都拥有权限
            var allPermission = _permissionService.GetAll();
            var existPermission = allPermission.FirstOrDefault(x => context.HttpContext.Request.Method.ToLower().Equals(x.ApiMethod.ToLower())
            && Regex.Match(context.HttpContext.Request.Path.Value.ToLower(), x.Url.ToLower()).Success);

            if (existPermission == null)
            {
                return;
            }

            //获取角色
            var roles = _userService.GetRoles(userName);

            var elePermissions = new Dictionary<long, AccountPermissionsDto>();
            foreach (var roleName in roles)
            {
                //获取角色列表
                var _rolePermissions = _rolePermissionService.GetRolePermissions(roleName);
                //合并角色重复的权限
                foreach (var permission in _rolePermissions)
                {
                    if (!elePermissions.ContainsKey(permission.Id))
                    {
                        elePermissions.Add(permission.Id, permission);
                    }
                }
            }

            //是否有权限
            var hasPermission = elePermissions.Values.FirstOrDefault(x => context.HttpContext.Request.Method.ToLower().Equals(x.ApiMethod.ToLower())
            && Regex.Match(context.HttpContext.Request.Path.Value.ToLower(), x.Url.ToLower()).Success);

            //此API无访问权限
            if (hasPermission == null)
            {
                var result = new ForbidResult();
                context.Result = result;

                //审核日志
                EleAuditLog auditLog = AuditLogHelper.GenerateRequestAuditLog(context, _userManager);
                auditLog.ReturnValue = JsonConvert.SerializeObject(result);
                _auditLogService.Add(auditLog);
            }
        }
    }
}
