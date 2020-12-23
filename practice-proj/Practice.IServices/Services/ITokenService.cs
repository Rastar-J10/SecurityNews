using System.Security.Claims;

namespace Practice.Services
{
    /// <summary>
    /// token业务接口
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        string CreateToken(Claim[] claims);
    }
}
