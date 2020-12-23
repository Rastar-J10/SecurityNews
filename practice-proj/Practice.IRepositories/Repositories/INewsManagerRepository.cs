using Practice.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice.Repositories
{
    /// <summary>
    /// 新闻管理仓储接口
    /// </summary>
    public interface INewsManagerRepository
    {
        /// <summary>
        /// 新闻管理界面数据查询
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> SelectNewsManager(int status, int pageIndex,long userId);

        /// <summary>
        /// 根据标题查询登录用户编辑的新闻
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> SelectNewsManagerTitle(string title, int pageIndex, long userId);

        /// <summary>
        /// 新建新闻
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Insert(NewsDetailEntity entity);

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> State(long id,int status,long userId);

        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update(NewsDetailEntity entity);
    }
}
