using System.Threading.Tasks;
using Practice.Entities;

namespace Practice.Repositories
{
    /// <summary>
    /// 验证码仓储接口
    /// </summary>
    public interface IVerifyCodeRepository
    {
        /// <summary>
        /// 将验证码添加到数据库
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="guid">唯一参数</param>
        /// <returns></returns>
        Task<bool> Insert(string code, string guid);

        /// <summary>
        /// 判断是否有相同的guid
        /// </summary>
        /// <param name="guid">唯一参数</param>
        /// <returns></returns>
        Task<bool> IsExist(string guid);

        /// <summary>
        /// 更改验证码状态
        /// </summary>
        /// <param name="id">验证码</param>
        /// <param name="status">验证码状态</param>
        /// <returns></returns>
        Task<bool> UpdateStatus(long id, int status);

        /// <summary>
        /// 查询验证码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<VerifyCodeEntity> GetVerify(string code, string guid);
    }
}
