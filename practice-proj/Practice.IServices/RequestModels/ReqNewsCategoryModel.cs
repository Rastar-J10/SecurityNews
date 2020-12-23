using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Practice.RequestModels
{
    /// <summary>
    /// 新闻类别请求model
    /// </summary>
    [Serializable]
    public class ReqNewsCategoryModel
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入类别名称")]
        [RegularExpression("^[\u4E00-\u9FA5]{2,4}$", ErrorMessage = "类别名称格式不正确")]
        public string Name { get; set; }

        /// <summary>
        /// 分类上级ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 修改新闻分类请求model
    /// </summary>
    [Serializable]
    public class ReqNewsCategoryChangeModel
    {
        /// <summary>
        /// 新闻分类ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入类别名称")]
        [RegularExpression("^[\u4E00-\u9FA5]{2,4}$", ErrorMessage = "类别名称格式不正确")]
        public string Name { get; set; }

        /// <summary>
        /// 分类上级ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 类别推荐请求model
    /// </summary>
    [Serializable]
    public class ReqNewsCategorySortModel
    {
        /// <summary>
        /// 新闻分类ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 分类排序
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 删除类别请求model
    /// </summary>
    [Serializable]
    public class ReqNewsCategoryDeleteModel
    {
        /// <summary>
        /// 新闻分类ID
        /// </summary>
        public long Id { get; set; }
    }
}
