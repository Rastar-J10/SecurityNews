using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.ResponseModels
{
    /// <summary>
    /// 新闻分类
    /// </summary>
    [Serializable]
    public class ResNewsCategoryModel
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
        /// 推荐值
        /// </summary>
        public int Sort { get; set; }
    }
}
