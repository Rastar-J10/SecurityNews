using Practice.ResponseModels;
using System.Threading.Tasks;
using Practice.RequestModels;

namespace Practice.Services
{
    /// <summary>
    /// 用户账户业务接口
    /// </summary>
    public interface IUserAccountService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="reqLogin">登录请求参数</param>
        /// <returns></returns>
        Task<ResModel<ResUserLoginModel>> Login(ReqLoginModel reqLogin);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="reqRegister">注册请求参数</param>
        /// <returns></returns>
        Task<ResModel<bool>> Register(ReqRegisterModel reqRegister);
    }
}
