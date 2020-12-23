using System;
using System.ComponentModel.DataAnnotations;

namespace Practice.RequestModels
{
    /// <summary>
    /// 新建新闻请求model
    /// </summary>
    [Serializable]
    public class ReqNewsDetailModel
    {
        /// <summary>
        /// 文章一级编号
        /// </summary>
        public int Category1 { get; set; }
        /// <summary>
        /// 文章二级编号
        /// </summary>
        public int Category2 { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入标题")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "标题长度在1-30之间")]
        public string Title { get; set; }
        /// <summary>
        /// 文章作者
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入作者")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "作者名长度在1-8之间")]
        public string Author { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入正文")]
        public string Content { get; set; }
        /// <summary>
        /// 文章摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 文章封面
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请上传封面")]
        public string Cover { get; set; }
        /// <summary>
        /// 文章封面2
        /// </summary>
        public string Cover2 { get; set; }
        /// <summary>
        /// 文章封面3
        /// </summary>
        public string Cover3 { get; set; }
        /// <summary>
        /// 文章标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatorId { get; set; }
        /// <summary>
        /// 最后操作人
        /// </summary>
        public int Operator { get; set; }
    }

    /// <summary>
    /// 更改新闻状态请求model
    /// </summary>
    [Serializable]
    public class ReqNewsDetailStateModel
    { 
        /// <summary>
        /// 新闻ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 新闻状态
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 修改新闻请求model
    /// </summary>
    [Serializable]
    public class ReqNewsDetailUpdateModel
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 文章一级编号
        /// </summary>
        public int Category1 { get; set; }
        /// <summary>
        /// 文章二级编号
        /// </summary>
        public int Category2 { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入标题")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "标题长度在1-30之间")]
        public string Title { get; set; }
        /// <summary>
        /// 文章作者
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入作者")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "作者名长度在1-8之间")]
        public string Author { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入正文")]
        public string Content { get; set; }
        /// <summary>
        /// 文章摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 文章封面
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请上传封面")]
        public string Cover { get; set; }
        /// <summary>
        /// 文章封面2
        /// </summary>
        public string Cover2 { get; set; }
        /// <summary>
        /// 文章封面3
        /// </summary>
        public string Cover3 { get; set; }
        /// <summary>
        /// 文章标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 最后操作人
        /// </summary>
        public int Operator { get; set; }
    }
}
