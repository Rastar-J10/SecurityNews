using Autofac;
using Autofac.AttributeExtensions;

namespace Practice
{
    /// <summary>
    /// 服务模块注入
    /// </summary>
    public class ServicesModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAttributedClasses(ThisAssembly);
        }
    }
}
