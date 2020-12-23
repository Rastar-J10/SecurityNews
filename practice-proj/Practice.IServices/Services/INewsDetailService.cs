using System.Collections.Generic;
using System.Threading.Tasks;
using Practice.ResponseModels;
using Practice.RequestModels;

namespace Practice.Services
{
    /// <summary>
    /// 新闻查询业务接口
    /// </summary>
    public interface INewsDetailService
    {
        /// <summary>
        /// 根据标题查询新闻
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> SelectDetailsTitle(string title, int pageIndex);

        /// <summary>
        /// 根据新闻分类查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<ResNewsDetailsCategoryModel>>> SelectDetailsCategory(int id,int pageIndex);

        /// <summary>
        /// 查询新闻正文
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<ResModel<ResNewsDetailModel>> SelectDetailsContent(int id,int status);

        /// <summary>
        /// 查询后台首页数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> BackGroundPage(long userId, int status, int pageIndex);

        /// <summary>
        /// 分类推荐查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<ResClassifyRecommendModel>>> ClassifyRecommend(int id);
    }
}
