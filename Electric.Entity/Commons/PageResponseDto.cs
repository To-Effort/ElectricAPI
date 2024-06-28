namespace Electric.Entity.Commons
{
    /// <summary>
    /// 翻页响应实体
    /// </summary>
    public class PageResponseDto
    {
        /// <summary>
        /// 每页记录数量
        /// </summary>
        public int PrePage { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}
