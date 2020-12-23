using AutoMapper;
using Practice.Entities;
using Practice.ResponseModels;
using Practice.RequestModels;

namespace Practice.MapperProfiles
{
    /// <summary>
    /// 维护model映射关系
    /// </summary>
    public class CommonProfile : Profile
    {
        /// <summary>
        /// constructor
        /// </summary>
        public CommonProfile()
        {
            CreateMap<UserAccountEntity, ResUserLoginModel>();
            //新闻分类映射
            CreateMap<NewsCategoryEntity, ResNewsCategoryModel>();
            //按新闻类别查询映射
            CreateMap<NewsDetailEntity, ResNewsDetailsCategoryModel>();
            //新闻正文映射
            CreateMap<NewsDetailEntity, ResNewsDetailModel>();
            //新建新闻映射
            CreateMap<ReqNewsDetailModel, NewsDetailEntity>();
            //修改新闻映射
            CreateMap<ReqNewsDetailUpdateModel, NewsDetailEntity>();
            //分类推荐映射
            CreateMap<NewsDetailEntity, ResClassifyRecommendModel>();
        }
    }
}
