using System.Security.Claims;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Common.Const;
using Practice.ResponseModels;
using Practice.Services;
using PracticeApi.Extensions.Base;

namespace PracticeApi.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Route("api/v{version:apiVersion}/test")]
    [ApiVersion("1.0")]
    public class TestController : BaseApiController
    {
        private readonly static ILog _log = LogManager.GetLogger(typeof(TestController));
        private readonly ITestService _testService;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// constructor
        /// </summary>
        public TestController(ITestService testService, 
            ITokenService tokenService)
        {
            _testService = testService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// 就是干
        /// <author>luoyi</author>
        /// </summary>
        /// <returns></returns>
        [HttpGet("do"), AllowAnonymous]
        public string Do() => _testService.Do();

        /// <summary>
        /// 创建token
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-token"), AllowAnonymous]
        public string Get() => _tokenService.CreateToken(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Role, "normal"),
            new Claim(ClaimTypesExConst.NickName, "普通用户")
        });

        /// <summary>
        /// token校验
        /// </summary>
        /// <returns></returns>
        [HttpGet("valid")]
        public object Validation() => new
        {
            UserId,
            Role, 
            NickName
        };

        /// <summary>
        /// 多版本
        /// </summary>
        /// <returns></returns>
        [HttpGet("version")]
        [ApiVersion("2.0")]
        public string Version() => "2.0接口";

        /// <summary>
        /// 从数据库取值(异步实现)
        /// </summary>
        /// <returns></returns>
        [HttpGet("async-value")]
        [AllowAnonymous]
        public async Task<ResUserLoginModel> GetValue() => await _testService.Get(1);

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <returns></returns>
        [HttpGet("log"), AllowAnonymous]
        public string Log()
        {
            _log.Info("输出日志");
            return "success";
        }
    }
}