using Electric.Entity.AuditLogs;
using Electric.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Electric.API.AuditLog
{
    /// <summary>
    /// 审核日志帮助类
    /// </summary>
    public class AuditLogHelper
    {
        /// <summary>
        /// 请求日志
        /// </summary>
        /// <param name="action"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public static EleAuditLog GenerateRequestAuditLog(ActionContext action, UserManager<EleUser> userManager)
        {
            //审核日志
            EleAuditLog auditLog = new EleAuditLog();
            var httpContext = action.HttpContext;
            var request = httpContext.Request;

            //当前用户
            var userName = httpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                var user = userManager.FindByNameAsync(userName).Result;
                auditLog.CreatorId = user.Id;
            }
            auditLog.ApiUrl = request.Path;
            auditLog.Method = request.Method;
            auditLog.AuditLogType = AuditLogType.Info;

            //ip
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress;
            auditLog.ClientIpAddress = remoteIpAddress == null ? string.Empty : remoteIpAddress.MapToIPv4().ToString();
            auditLog.BrowserInfo = request.Headers.ContainsKey("User-Agent") ? request.Headers["User-Agent"] : string.Empty;

            //获取参数
            var parameters = string.Empty;
            if (request.Method == "GET")
            {
                JObject json = new JObject();
                foreach (var item in request.Query)
                {
                    json.Add(item.Key, item.Value.ToString());
                }
                parameters = JsonConvert.SerializeObject(json);
            }
            else
            {
                var reader = new StreamReader(request.Body);
                request.Body.Position = 0;
                parameters = reader.ReadToEndAsync().Result;
                request.Body.Position = 0;
            }

            //敏感数据脱敏处理
            if (parameters.Length > 0)
            {
                var json = new JObject();
                foreach (var item in action.ActionDescriptor.Parameters)
                {
                    var value = GetParameterValue(item, action, parameters);
                    json.Add(item.Name, value);
                }
                auditLog.Parameters = JsonConvert.SerializeObject(json);
            }

            return auditLog;
        }

        /// <summary>
        /// 获取参数对应的值，并返回脱敏后的字符串
        /// </summary>
        /// <param name="parameterDescriptor"></param>
        /// <param name="action"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GetParameterValue(ParameterDescriptor parameterDescriptor, ActionContext action, string parameters)
        {
            var bindingSource = parameterDescriptor.BindingInfo.BindingSource;
            if (bindingSource == BindingSource.Path)
            {
                //根据路由获取参数的值
                return action.RouteData.Values.GetValueOrDefault(parameterDescriptor.Name).ToString();
            }
            else
            {
                //根据参数类型反序列化，获取参数对象
                var parameterObject = JsonConvert.DeserializeObject(parameters, parameterDescriptor.ParameterType);

                //重新序列化，获取脱敏处理的字符串
                return JsonConvert.SerializeObject(parameterObject);
            }
        }
    }
}