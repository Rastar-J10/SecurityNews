using Practice.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Practice.RequestModels;
using Practice.Entities;

namespace Practice.Services
{
    /// <summary>
    /// 新闻分类业务接口
    /// </summary>
    public interface INewsCategoryService
    {
        /// <summary>
        /// 查询新闻分类
        /// </summary>
        /// <returns></returns>
        Task<ResModel<IEnumerable<ResNewsCategoryModel>>> SelectCategory();

        /// <summary>
        /// 添加新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<bool>> Insert(ReqNewsCategoryModel model, long userId);

        /// <summary>
        /// 删除新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<bool>> Delete(ReqNewsCategoryDeleteModel model, long userId);

        /// <summary>
        /// 修改新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<bool>> Update(ReqNewsCategoryChangeModel model, long userId);

        /// <summary>
        /// 查询分类列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<ResNewsCategoryModel>>> SelectCategoryList(int id);

        /// <summary>
        /// 标题模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ResModel<IEnumerable<dynamic>>> SelectCategoryAll(int pageIndex, int pageSize, string name);

        /// <summary>
        /// 推荐值更改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResModel<bool>> RecommendSort(ReqNewsCategorySortModel model, long userId);
    }
}
