using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.ResponseModels;
using Practice.Services;
using PracticeApi.Extensions.Base;
using Practice.RequestModels;
using Microsoft.AspNetCore.Authorization;

namespace PracticeApi.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [Route("api/v{version:apiVersion}/UserAccount")]
    [ApiVersion("1.0")]
    public class UserAccountController : BaseApiController
    {
        private readonly IUserAccountService _userAccountService;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="userAccountService"></param>
        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("UserLogin")]
        [AllowAnonymous]
        public async Task<ResModel<ResUserLoginModel>> Login(ReqLoginModel model)
        {
            return await _userAccountService.Login(model);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost("UserRegister")]
        [AllowAnonymous]
        public async Task<ResModel<bool>> Register(ReqRegisterModel model)
        {
            return await _userAccountService.Register(model);
        }
    }
}
