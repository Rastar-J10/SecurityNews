using Practice.ResponseModels;
using System.Threading.Tasks;

namespace Practice.Services
{
    /// <summary>
    /// 测试业务接口
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        string Do();

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResUserLoginModel> Get(long id);
    }
}
