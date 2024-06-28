﻿using Electric.Service.UOW;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Electric.API.UOW
{
    /// <summary>
    /// 工作单元过滤特性
    /// </summary>
    public class UnitOfWorkFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 工作单元特性
        /// </summary>
        private UnitOfWorkAttribute? _unitOfWorkAttribute;

        /// <summary>
        /// 开启事务：在action方法运行之前调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _unitOfWorkAttribute = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x.GetType() == typeof(UnitOfWorkAttribute)) as UnitOfWorkAttribute;
            if (_unitOfWorkAttribute != null && !_unitOfWorkAttribute.IsTransactional)
            {
                return;
            }

            var _unitOfWork = context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as UnitOfWork;
            //开启事务
            _unitOfWork.BeginTransaction();
        }

        /// <summary>
        /// 提交事务：在action方法运行之后调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_unitOfWorkAttribute != null && !_unitOfWorkAttribute.IsTransactional)
            {
                return;
            }

            var _unitOfWork = context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as UnitOfWork;
            //开启事务
            _unitOfWork.Commit();
        }
    }
}
