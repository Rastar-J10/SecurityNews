using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.AttributeExtensions.Attributes
{
    public class SingleInstanceAttribute:RegistrationAttribute
    {
        protected SingleInstanceAttribute() : base(EnumLifeTimeScope.SingleInstance)
        {
        }
    }
}
