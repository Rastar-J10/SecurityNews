using Autofac.AttributeExtensions.Attributes;
using AutoMapper;
using Practice.Repositories;
using Practice.ResponseModels;
using System.Threading.Tasks;

namespace Practice.Services
{
    /// <summary>
    /// 测试业务类
    /// </summary>
    [InstancePerLifetimeScope]
    public class TestService : ITestService
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="testRepository"></param>
        public TestService(IMapper mapper,
            ITestRepository testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        public string Do()
        {
            return "一句话，就是干！";
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResUserLoginModel> Get(long id)
        {
            var data = await _testRepository.Get(id);
            return _mapper.Map<ResUserLoginModel>(data);
        }
    }
}
