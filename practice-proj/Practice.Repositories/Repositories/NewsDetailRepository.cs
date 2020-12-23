using Autofac.AttributeExtensions.Attributes;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Autofac.Features.AttributeFilters;
using Practice.Common.Const;
using System.Collections.Generic;
using Practice.Entities;

namespace Practice.Repositories
{
    /// <summary>
    /// 新闻查询仓储层
    /// </summary>
    [InstancePerLifetimeScope]
    public class NewsDetailRepository : INewsDetailRepository
    {
        private readonly IDbConnection _connection;
        /// <summary>
        /// 构造注入
        /// </summary>
        public NewsDetailRepository([KeyFilter(DbConst.TestMySqlDb)] IDbConnection connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// 根据新闻标题查询
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <param name="pageIndex">开始查询的条数</param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> SelectDetailsTitle(string title,int pageIndex)
        {
            //SQL语句
            var sql = $"select c.name,d.id,d.title,d.cover,d.readNum,d.commentCount,d.operateTime,d.author,(select COUNT(*) from news_detail where LOCATE(@title,title) and `status`=1) as count FROM news_category c,news_detail d where LOCATE(@title,d.title) and d.`status`=1 and c.id=d.category2 ORDER BY d.recommendSort DESC,d.operateTime DESC LIMIT @pageIndex,20";
            var result = await _connection.QueryAsync<dynamic>(sql,new { title,pageIndex});
            return result;
        }

        /// <summary>
        /// 根据新闻分类查询
        /// </summary>
        /// <param name="id">新闻类别ID</param>
        /// <param name="pageIndex">开始查询的条数</param>
        /// <returns></returns>
        public async Task<IEnumerable<NewsDetailEntity>> SelectDetailsCategory(int id, int pageIndex)
        {
            //SQL语句
            var sql = $"select `id`,`title`,`cover`,`cover2`,`cover3`,`operateTime`,`readNum`,`commentCount`,`author`,(select count(id) from news_detail where `category2`=@id and `status`=1) as count from news_detail where `category2`=@id and `status`=1 ORDER BY recommendSort DESC,operateTime DESC LIMIT @pageIndex,20";
            var result = await _connection.QueryAsync<NewsDetailEntity>(sql, new { id, pageIndex });
            return result;
        }

        /// <summary>
        /// 查询新闻正文
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<NewsDetailEntity> SelectDetailsContent(int id,int status)
        {
            //SQL语句
            var sql = $"select `id`,`category2`,`title`,`author`,`content`,`summary`,`cover`,`cover2`,`cover3`,`tags`,`readNum`,`operateTime` from news_detail where `status`=@status and `id`=@id";
            return await _connection.QuerySingleOrDefaultAsync<NewsDetailEntity>(sql, new { id,status });
        }

        /// <summary>
        /// 查询后台首页数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> BackGroundPage(long userId, int status,int pageIndex)
        {
            //SQL条件拼接语句
            var sqlWhere = "";
            if (status == 1 || status == 4)
            {
                sqlWhere += "`status` = @status ";

            }
            else
            {
                sqlWhere += "`status` !=0";
            }
            var sql = $"SELECT `id`,`status`,cover,title,readNum,commentCount,(SELECT SUM(readNum) FROM news_detail WHERE creatorId=@userId AND {sqlWhere})as cumRead,(SELECT SUM(commentCount) FROM news_detail WHERE creatorId=@userId AND {sqlWhere})as cumComment,(SELECT COUNT(id) FROM news_detail WHERE creatorId=@userId AND {sqlWhere})as cumNews,(select count(id) from news_detail where creatorId=@userId AND {sqlWhere}) as count from news_detail where creatorId=@userId AND {sqlWhere} order by operateTime desc LIMIT @pageIndex,10";
            return await _connection.QueryAsync<dynamic>(sql, new { userId, status, pageIndex });
        }

        /// <summary>
        /// 分类推荐查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NewsDetailEntity>> ClassifyRecommend(int id)
        {
            var sql = $"SELECT id,cover,title,author,readNum,commentCount from news_detail where category2=@id and `status`=1 ORDER BY recommend DESC,operateTime DESC LIMIT 5";
            return await _connection.QueryAsync<NewsDetailEntity>(sql, new { id });
        }

        /// <summary>
        /// 新闻阅读量自增
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> NewsReadNumIncrease(long id)
        {
            var sql = $"UPDATE news_detail SET readNum = readNum+1 WHERE id=@id";
            return await _connection.ExecuteAsync(sql, new { id })>0;
        }
    }
}
