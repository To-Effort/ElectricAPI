using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Electric.API.Exception
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class ElectricExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ElectricExceptionFilterAttribute> _logger;

        /// <summary>
        /// 依赖注入日志对象
        /// </summary>
        /// <param name="logger"></param>
        public ElectricExceptionFilterAttribute(ILogger<ElectricExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            //如果异常没有被处理则进行处理
            if (context.ExceptionHandled == false)
            {
                var str = $"异常：{context.HttpContext.Request.Path}{context.Exception.Message}";
                // 记录日志
                _logger.LogWarning(str);
                context.Result = new ContentResult
                {
                    // 返回状态码500，表示服务器异常
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Content = "服务器异常，请联系管理员"
                };
                //设置为true，表示异常被处理了
                context.ExceptionHandled = true;
            }
        }
    }
}
