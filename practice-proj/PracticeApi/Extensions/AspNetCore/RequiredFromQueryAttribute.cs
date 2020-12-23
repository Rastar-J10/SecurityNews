using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace PracticeApi.Extensions.AspNetCore
{
    /// <summary>
    /// 声明该 Query 参数是路由必须匹配
    /// </summary>
    internal class RequiredFromQueryAttribute : FromQueryAttribute, IParameterModelConvention
    {
        /// <summary>
        /// 约束的参数值，可以不提供，不提供参数值时，则要求有这个参数，如果提供则要求参数值也要匹配。
        /// </summary>
        public string Value { get; set; }

        public void Apply(ParameterModel parameter)
        {
            if (parameter.Action.Selectors != null && parameter.Action.Selectors.Any())
            {
                var constraint =
                    new RequiredFromQueryActionConstraint(parameter.BindingInfo?.BinderModelName ??
                                                          parameter.ParameterName, Value);
                parameter.Action.Selectors.Last().ActionConstraints.Add(constraint);
            }
        }
    }
}
