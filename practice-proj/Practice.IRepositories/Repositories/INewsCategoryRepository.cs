using System.Collections.Generic;
using System.Threading.Tasks;
using Practice.Entities;

namespace Practice.Repositories
{
    /// <summary>
    /// 新闻分类仓储接口
    /// </summary>
    public interface INewsCategoryRepository
    {
        /// <summary>
        /// 查询新闻分类
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NewsCategoryEntity>> SelectCategory();

        /// <summary>
        /// 是否有同名分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsCategoryName(string name);

        /// <summary>
        /// 判断一级分类数量是否大于9
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCategory();

        /// <summary>
        /// 判断二级分类是否大于5
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<int> IsChild(long parentId);

        /// <summary>
        /// 判断是否有这个一级分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsParentId(long id);

        /// <summary>
        /// 添加新闻分类
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <param name="sort"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        Task<bool> Insert(string name, long parentId, int sort, long operateId);

        /// <summary>
        /// 查询该id返回的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<NewsCategoryEntity> GetId(long id);

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Delete(long id,long userId);

        /// <summary>
        /// 修改新闻分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <param name="sort"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        Task<bool> Update(long id,string name, long parentId, int sort, long operateId);

        /// <summary>
        /// 查询分类列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<NewsCategoryEntity>> SelectCategoryList(int id);

        /// <summary>
        /// 标题模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> SelectCategoryAll(int pageIndex, int pageSize, string name);

        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> RecommendSort(long id, int sort, long userId);
    }
}
