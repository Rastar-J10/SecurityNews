using Practice.Entities;
using System.Threading.Tasks;

namespace Practice.Repositories
{
    /// <summary>
    /// 测试仓储接口
    /// </summary>
    public interface ITestRepository
    {
        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserAccountEntity> Get(long id);
    }
}
