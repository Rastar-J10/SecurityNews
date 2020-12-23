using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.AttributeExtensions.Attributes
{
    public class InstancePerRequestAttribute: InstancePerLifetimeScopeAttribute
    {
        protected InstancePerRequestAttribute() : base(RequestLifetimeScopeTag)
        {
        }
    }
}
