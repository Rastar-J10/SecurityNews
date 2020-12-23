using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PracticeApi.Extensions.Base
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// userId
        /// </summary>
        public long UserId => User.Identity.GetUserId<long>();

        /// <summary>
        /// 角色
        /// </summary>
        public string Role => User.Identity.GetRole<string>();

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName => User.Identity.GetNickName<string>();
    }
}
