namespace Electric.Entity.Commons
{
    /// <summary>
    /// 响应状态码
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,

        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 401,

        /// <summary>
        /// 未知资源
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 错误、异常
        /// </summary>
        Error = 500,
    }
}
