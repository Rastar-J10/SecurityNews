using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice.Repositories
{
    /// <summary>
    /// 操作日志仓储接口
    /// </summary>
    public interface IOperateLogRepository
    {
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="url"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Insert(string column, string action, string url, long userId);

        /// <summary>
        /// 按时间查询操作日志
        /// </summary>
        /// <param name="operateTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> SelectLogTime(string operateTime,int pageIndex, int pageSize);

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="account"></param>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> SelectLogText(string account, string column, string action, int pageIndex, int pageSize);

        /// <summary>
        /// 分类修改记录查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> CategoryEdit(long id, int pageIndex);
    }
}
