using Microsoft.AspNetCore.Mvc;
using PracticeApi.Extensions.Base;
using Practice.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Practice.ResponseModels;

namespace PracticeApi.Controllers
{
    /// <summary>
    /// 验证码控制器
    /// </summary>
    [Route("api/v{version:apiVersion}/VerifyCode")]
    [ApiVersion("1.0")]
    public class VerifyCodeController : BaseApiController
    {
        private readonly IVerifyCodeService _verifyCodeService;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="verifyCodeService"></param>
        public VerifyCodeController(IVerifyCodeService verifyCodeService)
        {
            _verifyCodeService = verifyCodeService;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GenerateVerifyCode")]
        [AllowAnonymous]
        public async Task<ResModel<ResVerifyCodeModel>> VerifyCode()
        {
            return await _verifyCodeService.Generate();
        }
    }
}
