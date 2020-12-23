using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.AttributeExtensions.Attributes
{
    public class InstancePerLifetimeScopeAttribute:RegistrationAttribute
    {
        public InstancePerLifetimeScopeAttribute() : base(EnumLifeTimeScope.InstancePerLifetimeScope)
        {
        }

        public InstancePerLifetimeScopeAttribute(params object[] lifetimeScopeTags):base(EnumLifeTimeScope.InstancePerLifetimeScope)
        {
            LifetimeScopeTags = lifetimeScopeTags;
        }
    }

}
