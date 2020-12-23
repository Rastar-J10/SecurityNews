using Autofac.AttributeExtensions.Attributes;
using AutoMapper;
using log4net;
using Practice.Repositories;
using Practice.RequestModels;
using Practice.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Practice.Common.Tools;
using System.Linq;
using System.Web;

namespace Practice.Services
{
    /// <summary>
    /// 新闻分类业务类
    /// </summary>
    [InstancePerLifetimeScope]
    public class NewsCategoryService : INewsCategoryService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NewsCategoryService));
        private readonly INewsCategoryRepository _newsCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IOperateLogService _operateLogService;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="newsCategoryRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="operateLogService"></param>
        public NewsCategoryService(INewsCategoryRepository newsCategoryRepository,
            IMapper mapper,
            IOperateLogService operateLogService)
        {
            _newsCategoryRepository = newsCategoryRepository;
            _mapper = mapper;
            _operateLogService = operateLogService;
        }

        /// <summary>
        /// 查询新闻分类
        /// </summary>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<ResNewsCategoryModel>>> SelectCategory()
        {
            try
            {
                var result = await _newsCategoryRepository.SelectCategory();
                //判断查询出来的值是否为空
                if (result == null)
                {
                    return ResModel.Failure<IEnumerable<ResNewsCategoryModel>>("查询数据为空，请重试");
                }
                var data = _mapper.Map<IEnumerable<ResNewsCategoryModel>>(result);
                return ResModel.Success(data, "查询成功，返回首页查看");
            }
            catch (Exception ex)
            {
                Log.Error($"查询失败" + ex);
                return ResModel.Failure<IEnumerable<ResNewsCategoryModel>>("查询失败");
            }
        }

        /// <summary>
        /// 添加新闻分类
        /// </summary>
        /// <param name="model">新闻分类请求参数</param>
        /// <param name="userId">操作人</param>
        /// <returns></returns>
        public async Task<ResModel<bool>> Insert(ReqNewsCategoryModel model, long userId)
        {
            try
            {
                //1.是否有重名的
                if (await _newsCategoryRepository.IsCategoryName(model.Name))
                {
                    return ResModel.Failure<bool>("分类名称不能相同");
                }

                //2.是否为添加一级分类
                if (model.ParentId == 0 && await _newsCategoryRepository.IsCategory())
                {
                    return ResModel.Failure<bool>("一级分类的数量已达最大，不可再添加");
                }

                //3.是否有此一级分类的id
                if (model.ParentId != 0 && await _newsCategoryRepository.IsParentId(model.ParentId))
                {
                    return ResModel.Failure<bool>("没有此一级分类，请重新选择");
                }

                //4.一级分类下的子类是否大于等于5
                if (model.ParentId != 0 && await _newsCategoryRepository.IsChild(model.ParentId) >= 5)
                {
                    return ResModel.Failure<bool>("该二级分类的数量已达最大，请重新选择分类");
                }

                var res = await _newsCategoryRepository.Insert(model.Name, model.ParentId, model.Sort, userId);

                // 添加操作日志
                await _operateLogService.AddLog("分类管理", "添加分类", "AddNewsCategory", userId);

                return ResModel.Success(res, "添加成功");
            }
            catch (Exception ex)
            {
                Log.Error($"添加出现异常" + ex);
                return ResModel.Failure<bool>("添加失败");
            }
        }

        /// <summary>
        /// 删除新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<bool>> Delete(ReqNewsCategoryDeleteModel model, long userId)
        {
            try
            {
                //1.是否有此id
                var data = await _newsCategoryRepository.GetId(model.Id);
                if (data.Id == 0)
                {
                    return ResModel.Failure<bool>("没有此分类，请重新选择");
                }

                //2.该ID是否为一级分类并有子级分类
                if (data.ParentId == 0 && await _newsCategoryRepository.IsChild(model.Id) > 0)
                {
                    return ResModel.Failure<bool>("该分类下有子级分类，请重新选择或删除子级");
                }

                // 添加操作日志
                await _operateLogService.AddLog("分类管理", "删除分类", "DeleteNewsCategory", userId);

                return ResModel.Success(await _newsCategoryRepository.Delete(model.Id, userId), "删除成功");
            }
            catch (Exception ex)
            {
                Log.Error($"删除出现异常" + ex);
                return ResModel.Failure<bool>("删除失败");
            }
        }

        /// <summary>
        /// 修改新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<bool>> Update(ReqNewsCategoryChangeModel model, long userId)
        {
            try
            {
                //1.是否有此分类
                var info = await _newsCategoryRepository.GetId(model.Id);
                if (info.Id == 0)
                {
                    return ResModel.Failure<bool>("没有此分类，请重新选择");
                }

                //2.是否有重名的分类
                if (await _newsCategoryRepository.IsCategoryName(model.Name))
                {
                    return ResModel.Failure<bool>("分类名称不能相同");
                }

                //3.是否修改为一级分类
                if (model.ParentId == 0 && await _newsCategoryRepository.IsCategory())
                {
                    return ResModel.Failure<bool>("一级分类的数量已达最大，不可再添加");
                }

                //4.修改上级ID时查询是否有此一级分类的
                if (model.ParentId != 0 && await _newsCategoryRepository.IsParentId(model.ParentId))
                {
                    return ResModel.Failure<bool>("没有此一级分类，请重新选择");
                }

                //5.一级分类下的子类是否大于等于5
                if (await _newsCategoryRepository.IsChild(model.ParentId) >= 5)
                {
                    return ResModel.Failure<bool>("该二级分类的数量已达最大，请重新选择分类");
                }

                // 添加操作日志
                await _operateLogService.AddLog("分类管理", "修改分类", "UpdateNewsCategory", userId);

                return ResModel.Success(await _newsCategoryRepository.Update(model.Id, model.Name, model.ParentId, model.Sort, userId), "修改成功");
            }
            catch (Exception ex)
            {
                Log.Error($"修改出现异常" + ex);
                return ResModel.Failure<bool>("删除失败");
            }
        }

        /// <summary>
        /// 联动查询分类列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<ResNewsCategoryModel>>> SelectCategoryList(int id)
        {
            try
            {
                var data = await _newsCategoryRepository.SelectCategoryList(id);
                if (data.Count() == 0)
                {
                    return ResModel.Failure<IEnumerable<ResNewsCategoryModel>>("查询数据失败");
                }
                var result = _mapper.Map<IEnumerable<ResNewsCategoryModel>>(data);
                return ResModel.Success(result, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"查询出现异常" + ex);
                return ResModel.Failure<IEnumerable<ResNewsCategoryModel>>("查询失败");
            }
        }

        /// <summary>
        /// 查询详细分类数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> SelectCategoryAll(int pageIndex, int pageSize, string name)
        {
            try
            {
                name = HttpUtility.UrlDecode(name);
                //分页数据处理
                pageIndex = PageHelper.PageCurrent(pageIndex, pageSize);
                var data = await _newsCategoryRepository.SelectCategoryAll(pageIndex, pageSize, name);
                if (data.Count() == 0)
                {
                    return ResModel.Failure<IEnumerable<dynamic>>("查询无数据");
                }
                return ResModel.Success(data, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"查询出现异常" + ex);
                return ResModel.Failure<IEnumerable<dynamic>>("查询失败");
            }
        }

        /// <summary>
        /// 更改推荐值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<bool>> RecommendSort(ReqNewsCategorySortModel model, long userId)
        {
            try
            {
                //1.是否有此分类
                var info = await _newsCategoryRepository.GetId(model.Id);
                if (info.Id == 0)
                {
                    return ResModel.Failure<bool>("没有此分类，请重新选择");
                }

                // 添加操作日志
                await _operateLogService.AddLog("分类管理", "分类推荐", "RecommendSort", userId);

                return ResModel.Success(await _newsCategoryRepository.RecommendSort(model.Id, model.Sort, userId), "修改成功");
            }
            catch (Exception ex)
            {
                Log.Error($"修改出现异常" + ex);
                return ResModel.Failure<bool>("删除失败");
            }
        }
    }
}
