using System;
using System.Collections.Generic;
using System.Text;

namespace Autofac.AttributeExtensions.Attributes
{
    public class NamedAttribute : ParameterRegistrationAttribute
    {
        public NamedAttribute(string name)
        {
            Named = name;
        }
    }
}
