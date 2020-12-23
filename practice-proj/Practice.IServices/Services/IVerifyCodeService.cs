using Practice.ResponseModels;
using System.Threading.Tasks;

namespace Practice.Services
{
    /// <summary>
    /// 验证码业务接口
    /// </summary>
    public interface IVerifyCodeService
    {
        /// <summary>
        /// 更改验证码状态
        /// </summary>
        /// <param name="id">验证码ID</param>
        /// <param name="status">验证码状态</param>
        /// <returns></returns>
        Task<bool> UpdateStatus(long id, int status);

        /// <summary>
        /// 判断验证码是否可用
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="guid">唯一参数</param>
        /// <returns></returns>
        Task<long> IsAvailable(string code, string guid);

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        Task<ResModel<ResVerifyCodeModel>> Generate();
    }
}
