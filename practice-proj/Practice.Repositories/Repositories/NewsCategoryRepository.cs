using Autofac.AttributeExtensions.Attributes;
using Autofac.Features.AttributeFilters;
using Dapper;
using Practice.Common.Const;
using Practice.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Practice.Repositories
{
    /// <summary>
    /// 新闻分类仓储层
    /// </summary>
    [InstancePerLifetimeScope]
    public class NewsCategoryRepository : INewsCategoryRepository
    {
        private readonly IDbConnection _connection;
        /// <summary>
        /// 构造注入
        /// </summary>
        public NewsCategoryRepository([KeyFilter(DbConst.TestMySqlDb)] IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 添加新闻分类
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Insert(NewsCategoryEntity entity)
        {
            var sql = "insert into `news_category` (`name`,`parentId`,`sort`,`operator`) values(@Name,@ParentId,@Sort,@Operator);";
            var result = await _connection.ExecuteAsync(sql, entity);
            return result > 0;
        }

        /// <summary>
        /// 新闻分类查询
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NewsCategoryEntity>> SelectCategory()
        {
            //SQL语句,按推荐和添加时间排序，查询9条一级分类
            var sql = $"SELECT `id`,`name` from news_category WHERE `status`=1 and `parentId`=0 order by sort DESC,addTime asc limit 9";
            var result = await _connection.QueryAsync<NewsCategoryEntity>(sql);
            return result;
        }

        /// <summary>
        /// 查询一级分类数量
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCategory()
        {
            var sql = $"select COUNT(`id`) from news_category where `status`=1 and `parentId`=0";
            var result = await _connection.ExecuteScalarAsync<int>(sql);
            return result >= 9;
        }

        /// <summary>
        /// 查询是否有同名的分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> IsCategoryName(string name)
        {
            var sql = $"select COUNT(`id`) from news_category where `status`=1 and `name`=@name";
            var result = await _connection.ExecuteScalarAsync<int>(sql,new { name});
            return result > 0;
        }

        /// <summary>
        /// 查询二级分类的数量
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<int> IsChild(long parentId)
        {
            var sql = $"select COUNT(`id`) from news_category where `status`=1 and `parentId`=@parentId";
            var result = await _connection.ExecuteScalarAsync<int>(sql,new { parentId});
            return result;
        }

        /// <summary>
        /// 判断是否有此一级分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsParentId(long id)
        {
            var sql = $"select COUNT(`id`) from news_category where `status`=1 and `id`=@id and `parentId`=0";
            var result = await _connection.ExecuteScalarAsync<int>(sql,new { id});
            return result == 0;
        }

        /// <summary>
        /// 添加新闻分类
        /// </summary>
        /// <param name="name">分类名</param>
        /// <param name="sort">排序数字(越大越前,同级)</param>
        /// <param name="parentId">父级id</param>
        /// <param name="operateId">操作人</param>
        /// <returns></returns>
        public async Task<bool> Insert(string name, long parentId, int sort, long operateId)
        {
            var sql = "insert into `news_category` (`name`,`parentId`,`sort`,`operator`) values(@name,@parentId,@sort,@operateId);";
            var result = await _connection.ExecuteAsync(sql, new { name, parentId, sort, operateId });
                return result > 0;
        }

        /// <summary>
        /// 获取该id的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<NewsCategoryEntity> GetId(long id)
        {
            var sql = $"select `id`,`parentId` from news_category where `id`=@id and `status`=1";
            var result = await _connection.QueryFirstOrDefaultAsync<NewsCategoryEntity>(sql,new { id});
            return result ?? new NewsCategoryEntity();
        }

        /// <summary>
        /// 删除新闻分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(long id,long userId)
        {
            var sql = $"update news_category set `status`=0,`operator`=@userId,`operateTime`=Now() where `id`=@id";
            var result = await _connection.ExecuteAsync(sql, new { id ,userId});
            return result > 0;
        }

        /// <summary>
        /// 修改新闻分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <param name="sort"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, string name, long parentId, int sort, long operateId)
        {
            var sql = $"update news_category set `name`=@name,`parentId`=@parentId,`sort`=@sort,`operator`=@operateId,`operateTime`=Now() where `id`=@id";
            var result = await _connection.ExecuteAsync(sql, new { id, name,parentId,sort,operateId });
            return result > 0;
        }

        /// <summary>
        /// 分类列表联动查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NewsCategoryEntity>> SelectCategoryList(int id)
        {
            var sqlWhere = "";
            if (id == 0)
            {
                sqlWhere += "and `parentId`=0";
            }
            else
            {
                sqlWhere += "and `parentId`=@id";
            }
            var sql = $"select `id`,`name`,`parentId`,`sort` from news_category where `status`=1 {sqlWhere}";
            return await _connection.QueryAsync<NewsCategoryEntity>(sql, new { id });
        }

        /// <summary>
        /// 标题模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> SelectCategoryAll(int pageIndex, int pageSize, string name)
        {
            var sqlWhere = "";
            if (name != null && name != "")
            {
                sqlWhere += "and LOCATE(@name,name)";
            }
            var sql = $"select n.id,n.`name`,n.parentId,n.sort,n.operateTime,u.nickname,(SELECT COUNT(`id`) from news_category where `status`=1 {sqlWhere}) as count from user_account u,news_category n WHERE n.`status`=1 AND u.userId = n.operator {sqlWhere} ORDER BY n.addTime limit @pageIndex,@pageSize";
            return await _connection.QueryAsync<dynamic>(sql, new { pageIndex,pageSize, name });
        }

        /// <summary>
        /// 推荐值更改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> RecommendSort(long id, int sort,long userId)
        {
            var sql = $"update news_category set `sort`=@sort,`operator`=@userId,`operateTime`=Now() where `id`=@id";
            var result = await _connection.ExecuteAsync(sql, new { id, sort,userId});
            return result > 0;
        }
    }
}
