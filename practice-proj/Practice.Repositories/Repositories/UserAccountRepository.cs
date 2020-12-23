using Autofac.AttributeExtensions.Attributes;
using Autofac.Features.AttributeFilters;
using Practice.Common.Const;
using Practice.Entities;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Practice.Repositories
{
    /// <summary>
    /// 用户账户仓储类
    /// </summary>
    [InstancePerLifetimeScope]
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// 构造注入
        /// </summary>
        public UserAccountRepository([KeyFilter(DbConst.TestMySqlDb)] IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">用户账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<UserAccountEntity> Login(string account, string password)
        {
            var sql = $"select `userId`,`account`,`nickname`,`role`,`status` from user_account where `account`=@account and `password`=@password";
            var result = await _connection.QueryFirstOrDefaultAsync<UserAccountEntity>(sql,new { account,password});
            return result ?? new UserAccountEntity();
        }

        /// <summary>
        /// 账号是否重复
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> IsAccount(string account)
        {
            var sql = $"select `userId` from user_account where `account`=@account";
            var result = await _connection.ExecuteScalarAsync<long>(sql, new { account });
            return result > 0;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Register(UserAccountEntity entity)
        {
            var sql = "insert into user_account (`account`, `password`, `nickname`, `role`, `status`) values (@Account, @Password, @Nickname, 1, 1);";
            return await _connection.ExecuteAsync(sql, entity) > 0;
        }
    }
}
