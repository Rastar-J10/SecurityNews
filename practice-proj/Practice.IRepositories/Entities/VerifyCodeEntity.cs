using System;

namespace Practice.Entities
{
    /// <summary>
    /// 验证码
    /// </summary>
    [Serializable]
    public class VerifyCodeEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Randomcode { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 验证码的状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
    }
}
