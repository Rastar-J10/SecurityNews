using System.Threading.Tasks;
using Practice.Entities;

namespace Practice.Repositories
{
    /// <summary>
    /// 用户账户仓储接口
    /// </summary>
    public interface IUserAccountRepository
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<UserAccountEntity> Login(string account,string password);

        /// <summary>
        /// 账号是否重复
        /// </summary>
        /// <param name="account">账户</param>
        /// <returns></returns>
        Task<bool> IsAccount(string account);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Register(UserAccountEntity entity);
    }
}
