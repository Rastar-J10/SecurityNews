using AutoMapper;
using log4net;
using Practice.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Practice.Common.Tools;
using Practice.ResponseModels;
using Autofac.AttributeExtensions.Attributes;
using Practice.RequestModels;
using Practice.Entities;

namespace Practice.Services
{
    /// <summary>
    /// 新闻管理业务层
    /// </summary>
    [InstancePerLifetimeScope]
    public class NewsManagerService : INewsManagerService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NewsDetailService));
        private readonly INewsManagerRepository _newsManagerRepository;
        private readonly IMapper _mapper;
        private readonly IOperateLogService _operateLogService;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="newsManagerRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="operateLogService"></param>
        public NewsManagerService(INewsManagerRepository newsManagerRepository,
            IMapper mapper,
            IOperateLogService operateLogService)
        {
            _newsManagerRepository = newsManagerRepository;
            _mapper = mapper;
            _operateLogService = operateLogService;
        }

        /// <summary>
        /// 查询新闻管理界面数据
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> SelectNewsManager(int status, int pageIndex, long userId)
        {
            try
            {
                pageIndex = PageHelper.PageCurrent(pageIndex, 10);//分页数据处理
                var data = await _newsManagerRepository.SelectNewsManager(status, pageIndex, userId);
                if (data.Count() == 0)
                {
                    return ResModel.Failure<IEnumerable<dynamic>>("查询无数据，换个状态试试~");
                }
                var res = _mapper.Map<IEnumerable<dynamic>>(data);
                return ResModel.Success(res, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"查询失败" + ex);
                return ResModel.Failure<IEnumerable<dynamic>>("查询失败");
            }
        }

        /// <summary>
        /// 新闻管理界面标题模糊查询
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> SelectNewsManagerTitle(string title, int pageIndex, long userId)
        {
            try
            {
                pageIndex = PageHelper.PageCurrent(pageIndex, 10);//分页数据处理
                var data = await _newsManagerRepository.SelectNewsManagerTitle(title, pageIndex, userId);
                if (data.Count() == 0)
                {
                    return ResModel.Failure<IEnumerable<dynamic>>("查询无数据，换个关键字试试~");
                }
                return ResModel.Success(data, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"查询失败" + ex);
                return ResModel.Failure<IEnumerable<dynamic>>("查询失败");
            }
        }


        /// <summary>
        /// 新建新闻
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<bool>> Insert(ReqNewsDetailModel model, int userId)
        {
            try
            {
                //数据处理
                //1.如果摘要Summary为空则取正文Content的前120个字赋值给Summary
                model.Summary = model.Summary != "" || model.Summary != null ? model.Summary : model.Content.Length < 120 ? model.Content.Substring(0, model.Content.Length) : model.Content.Substring(0, 120);
                //2.标签验证
                if (model.Tags.IndexOf("，") >= 0 && model.Tags.Length > 2)//判断为多个标签的情况
                {
                    string[] split = model.Tags.Split(new Char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);//返回分割后的数组
                    if (split.Length <= 5)//判断标签的数量是否大于5
                    {
                        foreach (var item in split)//验证标签的字数是否超过4个
                        {
                            if (item.Length > 4)
                            {
                                return ResModel.Failure<bool>("每个标签的长度不能大于4");
                            }
                        }
                    }
                    else
                    {
                        return ResModel.Failure<bool>("一篇新闻最多只能有五个标签");
                    }
                }
                else if (model.Tags.IndexOf("，") < 0 && model.Tags.Length > 4)
                {
                    return ResModel.Failure<bool>("单个标签的长度不能大于4");
                }

                model.CreatorId = userId;
                model.Operator = userId;
                var data = _mapper.Map<NewsDetailEntity>(model);
                var result = await _newsManagerRepository.Insert(data);

                // 添加操作日志
                await _operateLogService.AddLog("新闻管理", "新建新闻", "NewsArticle", userId);

                return result ? ResModel.Success(true, "新建新闻成功，请回管理界面查看") : ResModel.Failure<bool>("新建新闻失败");
            }
            catch (Exception ex)
            {
                Log.Error($"新建新闻发生异常", ex);
                return ResModel.Failure<bool>("新建新闻失败，请稍后再试");
            }
        }

        /// <summary>
        /// 更改新闻状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<bool>> State(ReqNewsDetailStateModel model, long userId)
        {
            try
            {
                bool data = await _newsManagerRepository.State(model.Id,model.status ,userId);
                if (!data)
                {
                    return ResModel.Failure<bool>("更改失败，请稍后重试");
                }

                // 添加操作日志
                await _operateLogService.AddLog("新闻管理", "新闻状态更改", "NewsDetailState", userId);

                return ResModel.Success(data, "更改成功");
            }
            catch (Exception ex)
            {
                Log.Error($"更改失败" + ex);
                return ResModel.Failure<bool>("更改失败！");
            }
        }

        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResModel<bool>> Update(ReqNewsDetailUpdateModel model, int userId)
        {
            try
            {
                //数据处理
                //1.如果摘要Summary为空则取正文Content的前120个字赋值给Summary
                model.Summary = model.Summary != "" || model.Summary != null ? model.Summary : model.Content.Length < 120 ? model.Content.Substring(0, model.Content.Length) : model.Content.Substring(0, 120);
                //2.标签验证
                if (model.Tags.IndexOf("，") >= 0 && model.Tags.Length > 2)//判断为多个标签的情况
                {
                    string[] split = model.Tags.Split(new Char[] { '，' }, StringSplitOptions.RemoveEmptyEntries);//返回分割后的数组
                    if (split.Length <= 5)//判断标签的数量是否大于5
                    {
                        foreach (var item in split)//验证标签的字数是否超过4个
                        {
                            if (item.Length > 4)
                            {
                                return ResModel.Failure<bool>("每个标签的长度不能大于4");
                            }
                        }
                    }
                    else
                    {
                        return ResModel.Failure<bool>("一篇新闻最多只能有五个标签");
                    }
                }
                else if (model.Tags.IndexOf("，") < 0 && model.Tags.Length > 4)
                {
                    return ResModel.Failure<bool>("单个标签的长度不能大于4");
                }

                model.Operator = userId;
                var data = _mapper.Map<NewsDetailEntity>(model);
                var result = await _newsManagerRepository.Update(data);

                // 添加操作日志
                await _operateLogService.AddLog("新闻管理", "修改新闻", "NewsDetailsModify", userId);

                return result ? ResModel.Success(true, "修改新闻成功，请回管理界面查看") : ResModel.Failure<bool>("修改新闻失败");
            }
            catch (Exception ex)
            {
                Log.Error($"修改新闻发生异常", ex);
                return ResModel.Failure<bool>("修改新闻失败，请稍后再试");
            }
        }
    }
}
