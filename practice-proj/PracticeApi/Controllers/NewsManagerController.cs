using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeApi.Extensions.Base;
using Practice.Services;
using Practice.ResponseModels;
using Practice.RequestModels;
using System;
using Newtonsoft.Json;

namespace PracticeApi.Controllers
{
    /// <summary>
    /// 新闻详情控制器
    /// </summary>
    [Route("api/v{version:apiVersion}/NewsManager")]
    [ApiVersion("1.0")]
    public class NewsManagerController : BaseApiController
    {
        private readonly INewsManagerService _newsManagerService;

        /// <summary>
        /// 构造注入
        /// </summary>
        public NewsManagerController(INewsManagerService newsManagerService)
        {
            _newsManagerService = newsManagerService;
        }

        /// <summary>
        /// 查询新闻管理界面数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("SelectNewsManager")]
        public Task<ResModel<IEnumerable<dynamic>>> SelectNewsManager(int status,int pageIndex)
        {
            return _newsManagerService.SelectNewsManager(status, pageIndex, UserId);
        }

        /// <summary>
        /// 新闻管理界面标题模糊查询
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex">分页</param>
        /// <returns></returns>
        [HttpGet("SelectNewsManagerTitle")]
        public Task<ResModel<IEnumerable<dynamic>>> SelectNewsManagerTitle(string title, int pageIndex)
        {
            return _newsManagerService.SelectNewsManagerTitle(title,pageIndex,UserId);
        }

        /// <summary>
        /// 新建新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("NewsArticle")]
        public async Task<ResModel<bool>> NewsArticle(ReqNewsDetailModel model)
        {
            return await _newsManagerService.Insert(model, Convert.ToInt32(UserId));
        }

        /// <summary>
        /// 新闻状态更改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("NewsDetailState")]
        public async Task<ResModel<bool>> NewsState(ReqNewsDetailStateModel model)
        {
            return await _newsManagerService.State(model, UserId);
        }

        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("NewsDetailsModify")]
        public async Task<ResModel<bool>> NewsModify(ReqNewsDetailUpdateModel model)
        {
            return await _newsManagerService.Update(model, Convert.ToInt32(UserId));
        }
    }
}
