using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.AttributeExtensions.Attributes
{
    public enum EnumLifeTimeScope
    {
        InstancePerDependency,

        InstancePerLifetimeScope,
        
        SingleInstance
    }
}
