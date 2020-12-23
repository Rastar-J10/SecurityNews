using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.AttributeExtensions.Attributes;
using AutoMapper;
using log4net;
using Practice.Entities;
using Practice.Repositories;
using Practice.RequestModels;
using Practice.ResponseModels;
using Practice.Common.Tools;
using System.Linq;

namespace Practice.Services
{
    /// <summary>
    /// 新闻查询业务层
    /// </summary>
    [InstancePerLifetimeScope]
    public class NewsDetailService : INewsDetailService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NewsDetailService));
        private readonly INewsDetailRepository _newsDetailsRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="newsDetailsRepository"></param>
        /// <param name="mapper"></param>
        public NewsDetailService(INewsDetailRepository newsDetailsRepository,
            IMapper mapper)
        {
            _newsDetailsRepository = newsDetailsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据标题查询新闻
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> SelectDetailsTitle(string title, int pageIndex)
        {
            try
            {
                pageIndex = PageHelper.PageCurrent(pageIndex, 20);//分页数据处理
                var data = await _newsDetailsRepository.SelectDetailsTitle(title, pageIndex);
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
        /// 根据新闻分类查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<ResNewsDetailsCategoryModel>>> SelectDetailsCategory(int id, int pageIndex)
        {
            try
            {
                pageIndex = PageHelper.PageCurrent(pageIndex, 20);//分页数据处理
                var data = await _newsDetailsRepository.SelectDetailsCategory(id, pageIndex);
                if (data.Count() == 0)
                {
                    return ResModel.Failure<IEnumerable<ResNewsDetailsCategoryModel>>("查询无数据，换个分类试试~");
                }
                var res = _mapper.Map<IEnumerable<ResNewsDetailsCategoryModel>>(data);
                return ResModel.Success(res, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"查询失败" + ex);
                return ResModel.Failure<IEnumerable<ResNewsDetailsCategoryModel>>("查询失败");
            }
        }

        /// <summary>
        /// 查询新闻正文
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<ResModel<ResNewsDetailModel>> SelectDetailsContent(int id,int status)
        {
            try
            {
                var data = await _newsDetailsRepository.SelectDetailsContent(id,status);
                //判断是否查询出了数据
                if (data == null)
                {
                    return ResModel.Failure<ResNewsDetailModel>("该新闻不存在正文，请再选一个感兴趣的试试吧~");
                }
                var result = _mapper.Map<ResNewsDetailModel>(data);
                await _newsDetailsRepository.NewsReadNumIncrease(id);
                return ResModel.Success(result, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"查询失败" + ex);
                return ResModel.Failure<ResNewsDetailModel>("查询失败");
            }
        }

        /// <summary>
        /// 查询后台首页数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<dynamic>>> BackGroundPage(long userId, int status, int pageIndex)
        {
            try
            {
                pageIndex = PageHelper.PageCurrent(pageIndex, 10);//分页数据处理
                var data = await _newsDetailsRepository.BackGroundPage(userId, status, pageIndex);
                if (data.Count() == 0)
                {
                    return ResModel.Failure<IEnumerable<dynamic>>("查询无数据，请快去编辑一篇新的文章吧~");
                }
                return ResModel.Success(data, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"新建新闻发生异常", ex);
                return ResModel.Failure<IEnumerable<dynamic>>("数据接口异常，请稍后再试");
            }
        }

        /// <summary>
        /// 分类推荐查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResModel<IEnumerable<ResClassifyRecommendModel>>> ClassifyRecommend(int id)
        {
            try
            {
                var data = await _newsDetailsRepository.ClassifyRecommend(id);
                if (data.Count() == 0)
                {
                    return ResModel.Failure<IEnumerable<ResClassifyRecommendModel>>("数据接口异常，请重新尝试");
                }
                var res = _mapper.Map<IEnumerable<ResClassifyRecommendModel>>(data);
                return ResModel.Success(res, "查询成功");
            }
            catch (Exception ex)
            {
                Log.Error($"新建新闻发生异常", ex);
                return ResModel.Failure<IEnumerable<ResClassifyRecommendModel>>("数据接口异常，请稍后再试");
            }
        }
    }
}
