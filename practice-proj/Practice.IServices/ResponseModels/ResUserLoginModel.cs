using Practice.Common.Enum;
using System;

namespace Practice.ResponseModels
{
    /// <summary>
    /// 登录实体类
    /// </summary>
    [Serializable]
    public class ResUserLoginModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }

        ///<summary>
        ///昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        
        public EnumUserRole Role { get; set; }

        ///<summary>
        ///token密钥
        /// </summary>
        public string SecurityKey { get; set; }
    }
}
