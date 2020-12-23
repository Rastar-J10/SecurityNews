using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeApi.Extensions.Base;
using Practice.Services;
using Practice.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Practice.RequestModels;

namespace PracticeApi.Controllers
{
    /// <summary>
    /// 新闻分类控制器
    /// </summary>
    [Route("api/v{version:apiVersion}/NewsCategory")]
    [ApiVersion("1.0")]
    public class NewsCategoryController : BaseApiController
    {
        private readonly INewsCategoryService _newsCategoryService;

        /// <summary>
        /// 构造注入
        /// </summary>
        public NewsCategoryController(INewsCategoryService newsCategoryService)
        {
            _newsCategoryService = newsCategoryService;
        }

        /// <summary>
        /// 查询新闻分类
        /// </summary>
        /// <returns></returns>
        [HttpGet("SelectNewsCategory")]
        [AllowAnonymous]
        public async Task<ResModel<IEnumerable<ResNewsCategoryModel>>> SelectCategory()
        {
            return await _newsCategoryService.SelectCategory();
        }

        /// <summary>
        /// 添加新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddNewsCategory")]
        public async Task<ResModel<bool>> InsertCategory(ReqNewsCategoryModel model)
        {
            //判断角色
            if (Role != "Manager" && Role!= "SuperManager")
            {
                return ResModel.Failure<bool>("您没有权限进行此操作");
            }
            return await _newsCategoryService.Insert(model,UserId);
        }

        /// <summary>
        /// 删除新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("DeleteNewsCategory")]
        public async Task<ResModel<bool>> DeleteCategory(ReqNewsCategoryDeleteModel model)
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<bool>("您没有权限进行此操作");
            }
            return await _newsCategoryService.Delete(model, UserId);
        }

        /// <summary>
        /// 修改新闻分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UpdateNewsCategory")]
        public async Task<ResModel<bool>> UpdateCategory(ReqNewsCategoryChangeModel model)
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<bool>("您没有权限进行此操作");
            }
            return await _newsCategoryService.Update(model, UserId);
        }

        /// <summary>
        /// 联动查询分类列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("SelectCategoryList")]
        public async Task<ResModel<IEnumerable<ResNewsCategoryModel>>> SelectCategoryList(int id=0)
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<IEnumerable<ResNewsCategoryModel>>("您没有权限进行此操作");
            }
            return await _newsCategoryService.SelectCategoryList(id);
        }

        /// <summary>
        /// 查询详细分类数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("SelectCategoryAll")]
        public async Task<ResModel<IEnumerable<dynamic>>> SelectCategoryAll(int pageIndex, int pageSize, string name = "")
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<IEnumerable<dynamic>>("您没有权限进行此操作");
            }
            return await _newsCategoryService.SelectCategoryAll(pageIndex,pageSize,name);
        }

        /// <summary>
        /// 更改推荐值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("RecommendSort")]
        public async Task<ResModel<bool>> RecommendSort(ReqNewsCategorySortModel model)
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<bool>("您没有权限进行此操作");
            }
            return await _newsCategoryService.RecommendSort(model, UserId);
        }
    }
}
