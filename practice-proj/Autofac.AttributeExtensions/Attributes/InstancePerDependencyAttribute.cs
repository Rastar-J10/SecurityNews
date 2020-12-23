using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.AttributeExtensions.Attributes
{
    public class InstancePerDependencyAttribute:RegistrationAttribute
    {
        protected InstancePerDependencyAttribute() : base(EnumLifeTimeScope.InstancePerDependency)
        {
        }
    }
}
