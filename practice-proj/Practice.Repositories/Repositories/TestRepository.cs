using Autofac.AttributeExtensions.Attributes;
using Autofac.Features.AttributeFilters;
using Dapper;
using Practice.Common.Const;
using Practice.Entities;
using System.Data;
using System.Threading.Tasks;

namespace Practice.Repositories
{
    /// <summary>
    /// 测试仓储类
    /// </summary>
    [InstancePerLifetimeScope]
    public class TestRepository : ITestRepository
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="connection"></param>
        public TestRepository([KeyFilter(DbConst.TestMySqlDb)]IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserAccountEntity> Get(long id)
        {
            var sql = $"select userId,account,role from user_account where userId=@id;";
            var result = await _connection.QueryFirstOrDefaultAsync<UserAccountEntity>(sql, new { id });

            return result ?? new UserAccountEntity();
        }
    }
}
