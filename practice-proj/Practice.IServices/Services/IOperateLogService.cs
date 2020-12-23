using System.Collections.Generic;
using System.Threading.Tasks;
using Practice.ResponseModels;

namespace Practice.Services
{
    /// <summary>
    /// 操作日志业务接口
    /// </summary>
    public interface IOperateLogService
    {
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="url"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AddLog(string column, string action, string url, long userId);

        /// <summary>
        /// 按时间查询操作日志
        /// </summary>
        /// <param name="operateTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> SelectLogTime(string operateTime, int pageIndex, int pageSize);

        /// <summary>
        /// 多条件查询操作日志
        /// </summary>
        /// <param name="account"></param>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> SelectLogText(string account, string column, string action, int pageIndex, int pageSize);

        /// <summary>
        /// 分类修改记录查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> CategoryEdit(long id, int pageIndex);
    }
}
