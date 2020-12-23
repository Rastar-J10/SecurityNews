using System;

namespace Practice.ResponseModels
{
    /// <summary>
    /// 新闻详情
    /// </summary>
    [Serializable]
    public class ResNewsDetailModel
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public long Id { get; set; }
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
        /// 摘要
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
        /// 标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        public int ReadNum { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
    }
    
    /// <summary>
    /// 新闻分类查询返回model
    /// </summary>
    [Serializable]
    public class ResNewsDetailsCategoryModel
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻作者
        /// </summary>
        public string Author { get; set; }
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
        /// 阅读数
        /// </summary>
        public int ReadNum { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 数据总量
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 分类推荐查询返回model
    /// </summary>
    [Serializable]
    public class ResClassifyRecommendModel
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 新闻封面
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 阅读数
        /// </summary>
        public int ReadNum { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }
    }
}
