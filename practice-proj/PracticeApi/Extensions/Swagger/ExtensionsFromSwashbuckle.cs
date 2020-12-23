using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PracticeApi.Extensions.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PracticeApi.Extensions.Swagger
{
    /// <summary>
    /// 从 Swashbuckle 库拷贝的扩展类，然后自己加了一个 <see cref="ExtensionsFromSwashbuckle.GetGroupKey"/>
    /// </summary>
    internal static class ExtensionsFromSwashbuckle
    {
        public static ParameterInfo ParameterInfo(this ApiParameterDescription apiParameter)
        {
            var controllerParameterDescriptor = apiParameter.ParameterDescriptor as ControllerParameterDescriptor;
            return controllerParameterDescriptor?.ParameterInfo;
        }

        public static PropertyInfo PropertyInfo(this ApiParameterDescription apiParameter)
        {
            var modelMetadata = apiParameter.ModelMetadata;

            return (modelMetadata?.ContainerType != null)
                ? modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName)
                : null;
        }

        public static IEnumerable<object> CustomAttributes(this ApiParameterDescription apiParameter)
        {
            var parameterInfo = apiParameter.ParameterInfo();
            var parameterAttributes = (parameterInfo != null) ? parameterInfo.GetCustomAttributes(true) : Enumerable.Empty<object>();

            var propertyInfo = apiParameter.PropertyInfo();
            var propertyAttributes = (propertyInfo != null) ? propertyInfo.GetCustomAttributes(true) : Enumerable.Empty<object>();

            return parameterAttributes.Union(propertyAttributes);
        }

        [Obsolete("Use ParameterInfo(), PropertyInfo() and CustomAttributes() extension methods instead")]
        internal static void GetAdditionalMetadata(
            this ApiParameterDescription apiParameter,
            ApiDescription apiDescription,
            out ParameterInfo parameterInfo,
            out PropertyInfo propertyInfo,
            out IEnumerable<object> parameterOrPropertyAttributes)
        {
            parameterInfo = apiParameter.ParameterInfo();
            propertyInfo = apiParameter.PropertyInfo();
            parameterOrPropertyAttributes = apiParameter.CustomAttributes();
        }

        internal static string RelativePathSansQueryString(this ApiDescription apiDescription)
        {
            return apiDescription.RelativePath?.Split('?').First();
        }

        internal static string GetGroupKey(this ApiDescription apiDescription)
        {
            var constraints =
                apiDescription.ActionDescriptor.ActionConstraints.OfType<RequiredFromQueryActionConstraint>()
                    .ToList();
            if (constraints.Count > 0)
            {
                var queryString = string.Join("&", constraints.Select(constraint =>
                {
                    if (constraint.Value != null)
                    {
                        return $"{constraint.Parameter}={constraint.Value}";
                    }

                    return constraint.Parameter;
                }));
                return $"{apiDescription.RelativePathSansQueryString()}?{queryString}";
            }
            return apiDescription.RelativePathSansQueryString();
        }

        internal static bool IsFromPath(this ApiParameterDescription apiParameter)
        {
            return (apiParameter.Source == BindingSource.Path);
        }

        internal static bool IsFromBody(this ApiParameterDescription apiParameter)
        {
            return (apiParameter.Source == BindingSource.Body);
        }

        internal static bool IsFromForm(this ApiParameterDescription apiParameter)
        {
            var source = apiParameter.Source;
            var elementType = apiParameter.ModelMetadata?.ElementType;

            return (source == BindingSource.Form || source == BindingSource.FormFile)
                   || (elementType != null && typeof(IFormFile).IsAssignableFrom(elementType));
        }

        internal static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        internal static bool IsDefaultResponse(this ApiResponseType apiResponseType)
        {
            var propertyInfo = apiResponseType.GetType().GetProperty("IsDefaultResponse");
            if (propertyInfo != null)
            {
                return (bool)propertyInfo.GetValue(apiResponseType);
            }

            // ApiExplorer < 2.1.0 does not support default response.
            return false;
        }
    }
}
