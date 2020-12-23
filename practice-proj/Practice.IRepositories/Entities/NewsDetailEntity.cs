using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.Entities
{
    /// <summary>
    /// 新闻详情
    /// </summary>
    [Serializable]
    public class NewsDetailEntity
    {   /// <summary>
        /// 新闻id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 新闻一级编号
        /// </summary>
        public int Category1 { get; set; }
        /// <summary>
        /// 新闻二级编号
        /// </summary>
        public int Category2 { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 新闻摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 新闻封面
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 新闻封面2
        /// </summary>
        public string Cover2 { get; set; }
        /// <summary>
        /// 新闻封面3
        /// </summary>
        public string Cover3 { get; set; }
        /// <summary>
        /// 新闻标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 新闻状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        public int ReadNum { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int PraiseNum { get; set; }
        /// <summary>
        /// 留言次数
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int Recommend { get; set; }
        /// <summary>
        /// 推荐排序
        /// </summary>
        public int RecommendSort { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后操作人
        /// </summary>
        public int Operator { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 数据总量
        /// </summary>
        public int Count { get; set; }
    }
}
