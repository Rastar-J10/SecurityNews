using System;
using System.ComponentModel.DataAnnotations;

namespace Practice.RequestModels
{
    /// <summary>
    /// 登录请求参数
    /// </summary>
    [Serializable]
    public class ReqLoginModel
    {
        /// <summary>
        /// 帐号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入帐号")]
        [RegularExpression("^[0-9a-zA-Z]{5,12}$", ErrorMessage = "帐号格式不正确")]
        public string Account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入密码")]
        [RegularExpression("^[0-9a-zA-Z]{6,18}$", ErrorMessage = "密码格式不正确")]
        public string Password { get; set; }

        /// <summary>
        /// 唯一参数
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "参数错误")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "参数错误")]
        public string Guid { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入验证码")]
        [RegularExpression("^[0-9a-zA-Z]{4}$", ErrorMessage = "验证码格式不正确")]
        public string Code { get; set; }
    }

    /// <summary>
    /// 帐号注册model
    /// </summary>
    [Serializable]
    public class ReqRegisterModel
    {
        /// <summary>
        /// 帐号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入帐号")]
        [RegularExpression("^[0-9a-zA-Z]{5,12}$", ErrorMessage = "帐号格式不正确")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入密码")]
        [RegularExpression("^[0-9a-zA-Z]{6,18}$", ErrorMessage = "密码格式不正确")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入确认密码")]
        [RegularExpression("^[0-9a-zA-Z]{6,18}$", ErrorMessage = "确认密码格式不正确")]
        public string RePassword { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入昵称")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "昵称长度在2-20之间")]
        public string NickName { get; set; }

        /// <summary>
        /// 唯一参数
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "参数错误")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "参数错误")]
        public string Guid { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入验证码")]
        [RegularExpression("^[0-9a-zA-Z]{4}$", ErrorMessage = "验证码格式不正确")]
        public string Code { get; set; }
    }
}
