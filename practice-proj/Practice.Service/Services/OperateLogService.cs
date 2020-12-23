using Autofac.AttributeExtensions.Attributes;
using log4net;
using Practice.Repositories;
using Practice.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice.Common.Tools;

namespace Practice.Services
{
    /// <summary>
    /// 操作日志业务类
    /// </summary>
    [InstancePerLifetimeScope]
    public class OperateLogService : IOperateLogService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(OperateLogService));
        private readonly IOperateLogRepository _operateLogRepository;

        /// <summary>
        /// constructor
        /// </summary>
        public OperateLogService(IOperateLogRepository operateLogRepository)
        {
            _operateLogRepository = operateLogRepository;
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="url"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task AddLog(string column, string action, string url, long userId)
        {
            try
            {
                await _operateLogRepository.Insert(column, action, url, userId);
            }
            catch (Exception ex)
            {
                Log.Error($"添加操作日志发生异常，column: {column}，action: {action}，url {url}，userId: {userId}", ex);
            }
        }

        /// <summary>
        /// 按时间查询操作日志
        /// </summary>
        /// <param name="operateTime"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> SelectLogTime(string operateTime, int pageIndex, int pageSize)
        {
            try
            {
                //分页处理
                pageIndex = PageHelper.PageCurrent(pageIndex, pageSize);
                var data = await _operateLogRepository.SelectLogTime(operateTime, pageIndex, pageSize);
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
        /// 多条件查询操作日志
        /// </summary>
        /// <param name="account"></param>
        /// <param name="column"></param>
        /// <param name="action"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> SelectLogText(string account, string column, string action, int pageIndex, int pageSize)
        {
            try
            {
                //分页处理
                pageIndex = PageHelper.PageCurrent(pageIndex, pageSize);
                var data = await _operateLogRepository.SelectLogText(account, column, action, pageIndex, pageSize);
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
        /// 分类修改记录查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> CategoryEdit(long id, int pageIndex)
        {
            try
            {
                //分页处理
                pageIndex = PageHelper.PageCurrent(pageIndex, 3);
                var data = await _operateLogRepository.CategoryEdit(id,pageIndex);
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
    }
}
