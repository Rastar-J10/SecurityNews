using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.ResponseModels
{
    /// <summary>
    /// 验证码响应model
    /// </summary>
    [Serializable]
    public class ResVerifyCodeModel
    {
        /// <summary>
        /// 唯一参数
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 验证码图片base64字符串
        /// </summary>
        public string ImageBase64 { get; set; }
    }
}
