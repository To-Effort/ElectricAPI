namespace Electric.Entity.Commons
{
    /// <summary>
    /// 响应结果类
    /// </summary>
    public class ResponseResultDto
    {
        public ResponseResultDto()
        {
            Code = ResponseCode.Success;
            Message = "操作成功";
        }

        /// <summary>
        /// 响应代码
        /// </summary>
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="message"></param>
        public ResponseResultDto SetSuccess(string message = "成功")
        {
            Code = ResponseCode.Success;
            Message = message;

            return this;
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="message"></param>
        public ResponseResultDto SetError(string message = "错误")
        {
            Code = ResponseCode.Error;
            Message = message;

            return this;
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="message"></param>
        public ResponseResultDto SetNotFound(string message = "未知资源")
        {
            Message = message;
            Code = ResponseCode.NotFound;
            return this;
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="message"></param>
        public ResponseResultDto SetNoPermission(string message = "无权限")
        {
            Message = message;
            Code = ResponseCode.NoPermission;
            return this;
        }
    }
}
