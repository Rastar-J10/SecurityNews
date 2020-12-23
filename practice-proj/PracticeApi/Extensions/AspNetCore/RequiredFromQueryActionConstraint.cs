using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace PracticeApi.Extensions.AspNetCore
{
    /// <summary>
    /// Query 参数必须匹配
    /// </summary>
    internal class RequiredFromQueryActionConstraint : IActionConstraint
    {
        public string Parameter { get; }
        public string Value { get; }

        public RequiredFromQueryActionConstraint(string parameter, string value = null)
        {
            Parameter = parameter;
            Value = value;
        }

        public int Order => 999;

        public bool Accept(ActionConstraintContext context)
        {
            if (!context.RouteContext.HttpContext.Request.Query.ContainsKey(Parameter))
            {
                return false;
            }

            if (Value != null)
            {
                return context.RouteContext.HttpContext.Request.Query[Parameter] == Value;
            }

            return true;
        }
    }
}
