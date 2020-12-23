using Autofac.AttributeExtensions.Attributes;
using Autofac.Features.AttributeFilters;
using Dapper;
using Practice.Common.Const;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Practice.Repositories
{
    /// <summary>
    /// 操作日志仓储类
    /// </summary>
    [InstancePerLifetimeScope]
    public class OperateLogRepository : IOperateLogRepository
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// 构造注入
        /// </summary>
        public OperateLogRepository([KeyFilter(DbConst.TestMySqlDb)]IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="url"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Insert(string column, string action, string url, long userId)
        {
            var sql = "insert into operate_log (`column`, `action`, `interfaceUrl`, `operator`) values (@column, @action, @url, @userId);";
            return await _connection.ExecuteAsync(sql, new { column, action, url, userId }) > 0;
        }

        /// <summary>
        /// 按时间查询操作日志
        /// </summary>
        /// <param name="operateTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> SelectLogTime(string operateTime,int pageIndex,int pageSize)
        {
            //条件语句
            var sqlWhere = "";
            if (operateTime != "" && operateTime != null)
            {
                sqlWhere += "where (datediff (operateTime,@operateTime)=0)";
            }
            var sql = $"select *,(SELECT COUNT(id) from operate_log {sqlWhere}) AS count from operate_log {sqlWhere} limit @pageIndex,@pageSize";
            return await _connection.QueryAsync<dynamic>(sql, new { operateTime, pageIndex,pageSize });
        }

        /// <summary>
        /// 多条件查询操作日志
        /// </summary>
        /// <param name="account"></param>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> SelectLogText(string account, string column, string action, int pageIndex, int pageSize)
        {
            //条件语句
            var sqlWhere = "";
            sqlWhere += account == "" || account == null ? " and 1=1 " : " and account=@account ";
            sqlWhere += column == "" || column == null ? " and 1=1 " : " and `column`=@column ";
            sqlWhere += action == "" || action == null ? " and 1=1 " : " and action=@action ";
            var sql = $"select id,`column`,action,interfaceUrl,o.operateTime,u.account,(select COUNT(id) from operate_log o,user_account u WHERE o.operator=u.userId {sqlWhere}) as count from operate_log o,user_account u WHERE o.operator=u.userId {sqlWhere} LIMIT @pageIndex,@pageSize";
            var result = await _connection.QueryAsync<dynamic>(sql, new { account, column, action, pageIndex,pageSize });
            return result;
        }

        /// <summary>
        /// 多条件查询操作日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> CategoryEdit(long id,int pageIndex)
        {
            var sql = $"select id,operateTime,action,(SELECT COUNT(id) from operate_log where action='修改分类' and operator=1) as count,(SELECT nickname from user_account WHERE userId=1) as nickname from operate_log where action='修改分类' and operator=@id limit @pageIndex,3";
            var result = await _connection.QueryAsync<dynamic>(sql, new { id,pageIndex });
            return result;
        }
    }
}
