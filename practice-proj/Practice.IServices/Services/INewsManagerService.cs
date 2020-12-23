using Practice.RequestModels;
using Practice.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice.Services
{
    /// <summary>
    /// 新闻管理业务接口
    /// </summary>
    public interface INewsManagerService
    {
        /// <summary>
        /// 查询新闻管理界面数据
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> SelectNewsManager(int status, int pageIndex, long userId);

        /// <summary>
        /// 新闻管理界面标题模糊查询
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> SelectNewsManagerTitle(string title, int pageIndex, long userId);

        /// <summary>
        /// 新建新闻
        /// </summary>
        /// <param name="model">新建新闻请求参数</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        Task<ResModel<bool>> Insert(ReqNewsDetailModel model, int userId);

        /// <summary>
        /// 新闻状态更改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<bool>> State(ReqNewsDetailStateModel model, long userId);

        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<bool>> Update(ReqNewsDetailUpdateModel model,int userId);
    }
}
