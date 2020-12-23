using Autofac.AttributeExtensions.Attributes;
using log4net;
using Practice.Common.Tools;
using Practice.Repositories;
using Practice.ResponseModels;
using System;
using System.Threading.Tasks;

namespace Practice.Services
{
    /// <summary>
    /// 验证码业务层
    /// </summary>
    [InstancePerLifetimeScope]
    public class VerifyCodeService : IVerifyCodeService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VerifyCodeService));
        private readonly IVerifyCodeRepository _verifyCodeRepository;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="verifyCodeRepository"></param>
        public VerifyCodeService(IVerifyCodeRepository verifyCodeRepository)
        {
            _verifyCodeRepository = verifyCodeRepository;
        }

        /// <summary>
        /// 更改验证码状态
        /// </summary>
        /// <param name="id">验证码ID</param>
        /// <param name="status">验证码状态</param>
        /// <returns></returns>
        public async Task<bool> UpdateStatus(long id, int status)
        {
            try
            {
                return await _verifyCodeRepository.UpdateStatus(id, status);
            }
            catch (Exception ex)
            {
                Log.Error($"修改验证码状态发生异常，id: {id}", ex);
                return false;
            }
        }
        
        /// <summary>
        /// 判断验证码是否可用
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="guid">唯一参数</param>
        /// <returns></returns>
        public async Task<long> IsAvailable(string code, string guid)
        {
            try
            {
                var result = await _verifyCodeRepository.GetVerify(code, guid);
                //验证码不能为空，状态要为未使用，时间不能超过30分钟
                return result.Id > 0 && result.Status == 0 && (DateTime.Now - result.Addtime).TotalSeconds<1800 ?result.Id :0;
            }
            catch (Exception ex)
            {
                Log.Error("验证码较验发生异常", ex);
                return 0;
            }
            
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public async Task<ResModel<ResVerifyCodeModel>> Generate()
        {
            try
            {
                //生成唯一参数guid
                var guid = Guid.NewGuid().ToString();
                //判断数据库中是否有相同guid
                var isExist = await _verifyCodeRepository.IsExist(guid);
                if (isExist)
                {
                    return ResModel.Failure<ResVerifyCodeModel>("请点击验证码重新生成");
                }

                //生成验证码和图片
                var code = VerifyCodeHelper.GenerateRandomCode(4);
                var imgBytes = VerifyCodeHelper.CodeImage(code.ToUpper());

                //把验证码添加到数据库中
                if (!await _verifyCodeRepository.Insert(code.ToUpper(), guid))
                {
                    return ResModel.Failure<ResVerifyCodeModel>("请点击验证码重新生成");
                }

                //返回验证码和图片
                return ResModel.Success(new ResVerifyCodeModel
                {
                    Guid = guid,
                    ImageBase64 = $"data:image/gif;base64,{Convert.ToBase64String(imgBytes)}"
                });
            }
            catch (Exception ex)
            {
                Log.Error("生成验证码图片发生异常", ex);
                return ResModel.Failure<ResVerifyCodeModel>("请点击刷新验证码，重新生成");
            }
        }
    }
}
