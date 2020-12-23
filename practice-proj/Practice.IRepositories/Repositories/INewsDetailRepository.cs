using System.Collections.Generic;
using System.Threading.Tasks;
using Practice.Entities;

namespace Practice.Repositories
{
    /// <summary>
    /// 新闻查询仓储接口
    /// </summary>
    public interface INewsDetailRepository
    {
        /// <summary>
        /// 根据新闻标题查询
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> SelectDetailsTitle(string title,int pageIndex);

        /// <summary>
        /// 根据新闻分类查询
        /// </summary>
        /// <param name="id">新闻分类ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        Task<IEnumerable<NewsDetailEntity>> SelectDetailsCategory(int id, int pageIndex);

        /// <summary>
        /// 查询新闻正文
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<NewsDetailEntity> SelectDetailsContent(int id,int status);

        /// <summary>
        /// 后台首页数据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">新闻状态</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> BackGroundPage(long userId, int status,int pageIndex);

        /// <summary>
        /// 分类推荐
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<NewsDetailEntity>> ClassifyRecommend(int id);

        /// <summary>
        /// 新闻阅读量增加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> NewsReadNumIncrease(long id);
    }
}
