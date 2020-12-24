using Autofac.AttributeExtensions.Attributes;
using Autofac.Features.AttributeFilters;
using Dapper;
using Practice.Common.Const;
using Practice.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Repositories
{
    /// <summary>
    /// 新闻管理仓储层
    /// </summary>
    [InstancePerLifetimeScope]
    public class NewsManagerRepository : INewsManagerRepository
    {
        private readonly IDbConnection _connection;
        /// <summary>
        /// 构造注入
        /// </summary>
        public NewsManagerRepository([KeyFilter(DbConst.TestMySqlDb)] IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 查询新闻管理界面数据
        /// </summary>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> SelectNewsManager(int status, int pageIndex, long userId)
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
            //SQL语句
            var sql = $"select id,cover,title,`status`,operateTime,(select COUNT(id) from news_detail where operator=@userId AND {sqlWhere}) as count from news_detail where operator=@userId AND {sqlWhere} order by operateTime desc LIMIT @pageIndex,10";
            var result = await _connection.QueryAsync<dynamic>(sql, new { status, pageIndex,userId });
            return result;
        }

        /// <summary>
        /// 新闻管理界面标题模糊查询
        /// </summary>
        /// <param name="title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> SelectNewsManagerTitle(string title, int pageIndex, long userId)
        {
            //SQL语句
            var sql = $"select id,cover,title,`status`,operateTime,(select COUNT(id) from news_detail where operator=@userId AND LOCATE(@title,title)) as count from news_detail where operator=@userId AND LOCATE(@title,title) LIMIT @pageIndex,10";
            var result = await _connection.QueryAsync<dynamic>(sql, new { title, pageIndex, userId });
            return result;
        }

        /// <summary>
        /// 新建新闻
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Insert(NewsDetailEntity entity)
        {
            var sql = $"insert into news_detail(`category1`,`category2`,`title`,`author`,`content`,`summary`,`cover`,`cover2`,`cover3`,`tags`,`status`,`readNum`,`praiseNum`,`commentCount`,`recommend`,`recommendSort`,`creatorId`,`createTime`,`operator`,`operateTime`) VALUES(@Category1,@Category2,@Title,@Author,@Content,@Summary,@Cover,@Cover2,@Cover3,@Tags,4,0,0,0,default,default,@CreatorId,NOW(),@Operator,NOW())";
            return await _connection.ExecuteAsync(sql, entity) > 0;
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> State(long id,int status,long userId)
        {
            var sql = $"update news_detail set `status`=@status,operator=@userId,operateTime=NOW() where `id`=@id";
            return await _connection.ExecuteAsync(sql, new { id,status,userId }) > 0;
        }

        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(NewsDetailEntity entity)
        {
            var sql = $"update news_detail set category1=@Category1,category2=@Category2,title=@Title,author=@Author,content=@Content,summary=@Summary,cover=@Cover,cover2=@Cover2,cover3=@Cover3,tags=@Tags,operator=@Operator where id=@Id and `status`!=0";
            return await _connection.ExecuteAsync(sql, entity)>0;
        }
    }
}
