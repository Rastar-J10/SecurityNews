using System;

namespace Practice.Entities
{
    /// <summary>
    /// 用户帐号
    /// </summary>
    [Serializable]
    public class UserAccountEntity
    {
        /// <summary>
        /// userId
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
        public int Role { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 状态 0删除 1正常 2禁用
        /// </summary>
        public int Status { get; set; }
    }
}
