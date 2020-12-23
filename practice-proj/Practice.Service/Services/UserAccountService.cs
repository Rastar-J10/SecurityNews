using Autofac.AttributeExtensions.Attributes;
using AutoMapper;
using log4net;
using Practice.Repositories;
using Practice.RequestModels;
using Practice.ResponseModels;
using System.Threading.Tasks;
using Practice.Common.Tools;
using System.Security.Claims;
using Practice.Common.Const;
using System;
using Practice.Entities;

namespace Practice.Services
{
    /// <summary>
    /// 用户账户业务层
    /// </summary>
    [InstancePerLifetimeScope]
    public class UserAccountService : IUserAccountService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserAccountService));
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IVerifyCodeService _verifyCodeService;
        private readonly IOperateLogService _operateLogService;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="userAccountRepository">用户仓储</param>
        /// <param name="mapper">映射</param>
        /// <param name="tokenService">token</param>
        /// <param name="verifyCodeService">验证码</param>
        /// <param name="operateLogService">操作日志</param>
        public UserAccountService(IUserAccountRepository userAccountRepository,
            IMapper mapper,
            ITokenService tokenService,
            IVerifyCodeService verifyCodeService,
            IOperateLogService operateLogService)
        {
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _verifyCodeService = verifyCodeService;
            _operateLogService = operateLogService;
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="reqLogin">登录请求参数</param>
        /// <returns></returns>
        public async Task<ResModel<ResUserLoginModel>> Login(ReqLoginModel reqLogin)
        {
            try
            {
                //验证码验证
                var code = await _verifyCodeService.IsAvailable(reqLogin.Code, reqLogin.Guid);
                if (code <= 0)
                {
                    return ResModel.Failure<ResUserLoginModel>("验证码输入错误或已过期");
                }
                //将使用过的验证码状态更改
                await _verifyCodeService.UpdateStatus(code, 2);
                //账号验证
                var account = await _userAccountRepository.Login(reqLogin.Account, EncryptionHelper.MD5Hash(reqLogin.Password));
                if (account.UserId <= 0)
                {
                    return ResModel.Failure<ResUserLoginModel>("账号或密码错误，请重新输入");
                }
                else if (account.Status != 1)
                {
                    return ResModel.Failure<ResUserLoginModel>("该账户已被删除或禁用，请申请找回或换个账号登录");
                }
                //写入token
                var user = _mapper.Map<ResUserLoginModel>(account);
                user.SecurityKey = _tokenService.CreateToken(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypesExConst.NickName, user.Nickname)
                });

                // 添加操作日志
                await _operateLogService.AddLog("帐号管理", "登录", "login", user.UserId);

                return ResModel.Success(user, "登陆成功");
            }
            catch (Exception ex)
            {
                Log.Error($"登录发生异常, account: {reqLogin.Account}", ex);
                return ResModel.Failure<ResUserLoginModel>("登录失败，请稍后再试");
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="reqRegister">注册请求参数</param>
        /// <returns></returns>
        public async Task<ResModel<bool>> Register(ReqRegisterModel reqRegister)
        {
            try
            {
                //验证码验证
                var code = await _verifyCodeService.IsAvailable(reqRegister.Code, reqRegister.Guid);
                if (code <= 0)
                {
                    return ResModel.Failure<bool>("验证码输入错误或已过期");
                }
                //两次输入密码是否一致
                if (reqRegister.Password != reqRegister.RePassword)
                {
                    return ResModel.Failure<bool>("两次输入密码不一致，请重新输入");
                }
                //判断账号是否重复
                var account = await _userAccountRepository.IsAccount(reqRegister.Account);
                if (account)
                {
                    return ResModel.Failure<bool>("账号已重复，请重新输入账号");
                }
                var result = await _userAccountRepository.Register(new UserAccountEntity
                {
                    Account = reqRegister.Account,
                    Password = EncryptionHelper.MD5Hash(reqRegister.Password),
                    Nickname = reqRegister.NickName
                });
                //三目运算符判断是否注册成功
                return result ? ResModel.Success(true, "注册成功，请返回登录界面进行登录") : ResModel.Failure<bool>("注册失败");
            }
            catch (Exception ex)
            {
                Log.Error($"注册发生异常",ex);
                return ResModel.Failure<bool>("登录失败，请稍后再试");
            }
            
        }
    }
}
