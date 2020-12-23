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
    /// 验证码仓储层
    /// </summary>
    [InstancePerLifetimeScope]
    public class VerifyCodeRepository : IVerifyCodeRepository
    {
        private readonly IDbConnection _connection;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="connection"></param>
        public VerifyCodeRepository([KeyFilter(DbConst.TestMySqlDb)] IDbConnection connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// 查询验证码
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="guid">唯一参数</param>
        /// <returns></returns>
        public async Task<VerifyCodeEntity> GetVerify(string code, string guid)
        {
            var sql = "select `id`, `status`, `addtime` from `verification_code` where `guid`=@guid and `randomcode`=@code;";
            var result = await _connection.QueryFirstOrDefaultAsync<VerifyCodeEntity>(sql, new { code, guid });
            return result ?? new VerifyCodeEntity();
        }

        /// <summary>
        /// 添加验证码到数据库
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="guid">唯一参数</param>
        /// <returns></returns>
        public async Task<bool> Insert(string code, string guid)
        {
            var sql = "insert into `verification_code` (`randomcode`,`guid`,`status`,`addtime`) values (@code, @guid, 0, now());";
            var result = await _connection.ExecuteAsync(sql, new { code, guid });
            return result > 0;
        }

        /// <summary>
        /// 判断是否有相同的guid
        /// </summary>
        /// <param name="guid">唯一参数</param>
        /// <returns></returns>
        public async Task<bool> IsExist(string guid)
        {
            var sql = "select `id` from `verification_code` where `guid`=@guid limit 1;";
            var result = await _connection.ExecuteScalarAsync<long>(sql, new { guid });
            return result > 0;
        }

        /// <summary>
        /// 更改验证码的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStatus(long id, int status)
        {
            var sql = "update `verification_code` set `status`=@status where `id`=@id";
            return await _connection.ExecuteAsync(sql, new { id, status }) > 0;
        }
    }
}
