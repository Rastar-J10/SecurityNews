using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeApi.Extensions.Base;
using Practice.Services;
using Practice.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Practice.RequestModels;
using System;

namespace PracticeApi.Controllers
{
    /// <summary>
    /// 新闻详情控制器
    /// </summary>
    [Route("api/v{version:apiVersion}/NewsDetails")]
    [ApiVersion("1.0")]
    public class NewsDetailController : BaseApiController
    {
        private readonly INewsDetailService _newsDetailsService;

        /// <summary>
        /// 构造注入
        /// </summary>
        public NewsDetailController(INewsDetailService newsDetailsService)
        {
            _newsDetailsService = newsDetailsService;
        }

        /// <summary>
        /// 根据标题查询新闻
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        [HttpGet("SelectNewsDetailsTitle")]
        [AllowAnonymous]
        public async Task<ResModel<IEnumerable<dynamic>>> SelectDetails(string title,int pageIndex)
        {
            //判断标题是否为空
            if (title == "" || title == null)
            {
                return ResModel.Failure<IEnumerable<dynamic>>("标题不能为空，请重新输入");
            }
            return await _newsDetailsService.SelectDetailsTitle(title,pageIndex);
        }

        /// <summary>
        /// 根据新闻类别查询新闻
        /// </summary>
        /// <param name="id">新闻类别ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        [HttpGet("SelectNewsDetailsCategory")]
        [AllowAnonymous]
        public async Task<ResModel<IEnumerable<ResNewsDetailsCategoryModel>>> SelectDetailsCategory(int id, int pageIndex)
        {
            //判断选择的新闻类别是否为空
            if (id == 0)
            {
                return ResModel.Failure<IEnumerable<ResNewsDetailsCategoryModel>>("请选择新闻类别");
            }
            return await _newsDetailsService.SelectDetailsCategory(id, pageIndex);
        }

        /// <summary>
        /// 查询新闻正文
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("SelectNewsDetailsContent")]
        [AllowAnonymous]
        public async Task<ResModel<ResNewsDetailModel>> SelectDetailsContent(int id,int status)
        {
            if (id == 0)
            {
                return ResModel.Failure<ResNewsDetailModel>("请选择有效的新闻查看");
            }
            return await _newsDetailsService.SelectDetailsContent(id,status);
        }

        /// <summary>
        /// 查询后台首页数据
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        [HttpGet("SelectBackGroundPage")]
        public Task<ResModel<IEnumerable<dynamic>>> BackGroundPage(int status, int pageIndex)
        {
            return _newsDetailsService.BackGroundPage(UserId, status, pageIndex);
        }

        /// <summary>
        /// 分类推荐查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("SelectClassifyRecommend")]
        [AllowAnonymous]
        public async Task<ResModel<IEnumerable<ResClassifyRecommendModel>>> ClassifyRecommend(int id)
        {
            return await _newsDetailsService.ClassifyRecommend(id);
        }
    }
}
