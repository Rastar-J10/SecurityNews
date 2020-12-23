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
    [Route("api/v{version:apiVersion}/OpeaateLog")]
    [ApiVersion("1.0")]
    public class OpeaateLogController : BaseApiController
    {
        private readonly IOperateLogService _operateLogService;

        /// <summary>
        /// 构造注入
        /// </summary>
        public OpeaateLogController(IOperateLogService operateLogService)
        {
            _operateLogService = operateLogService;
        }

        /// <summary>
        /// 按时间查询操作日志
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="operateTime"></param>
        /// <returns></returns>
        [HttpGet("SelectLogTime")]
        public async Task<ResModel<IEnumerable<dynamic>>> SelectLogTime(int pageIndex, int pageSize, string operateTime = "")
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<IEnumerable<dynamic>>("您没有权限进行此操作");
            }
            return await _operateLogService.SelectLogTime(operateTime, pageIndex, pageSize);
        }

        /// <summary>
        /// 多条件查询操作日志
        /// </summary>
        /// <param name="account"></param>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("SelectLogText")]
        public async Task<ResModel<IEnumerable<dynamic>>> SelectLogText(string account, string column, string action, int pageIndex, int pageSize)
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<IEnumerable<dynamic>>("您没有权限进行此操作");
            }
            return await _operateLogService.SelectLogText(account, column, action, pageIndex, pageSize);
        }

        /// <summary>
        /// 分类修改记录查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet("CategoryEditRecord")]
        public async Task<ResModel<IEnumerable<dynamic>>> CategoryEdit(int pageIndex)
        {
            //判断角色
            if (Role != "Manager" && Role != "SuperManager")
            {
                return ResModel.Failure<IEnumerable<dynamic>>("您没有权限进行此操作");
            }
            return await _operateLogService.CategoryEdit(UserId, pageIndex);
        }
    }
}
