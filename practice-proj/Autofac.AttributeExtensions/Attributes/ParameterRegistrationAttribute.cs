using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.AttributeExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ParameterRegistrationAttribute:Attribute
    {
        public string Named { get; set; }
    }
}
