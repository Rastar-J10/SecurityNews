using System;

namespace Practice.Entities
{
    /// <summary>
    /// 新闻分类
    /// </summary>
    [Serializable]
    public class NewsCategoryEntity
    {
        /// <summary>
        /// 新闻分类ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 新闻分类上级分类ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 新闻分类排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 新闻分类添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 新闻分类最后操作人
        /// </summary>
        public long Operator { get; set; }
        /// <summary>
        /// 新闻分类最后操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 新闻分类状态
        /// </summary>
        public int Status { get; set; }
    }
}
