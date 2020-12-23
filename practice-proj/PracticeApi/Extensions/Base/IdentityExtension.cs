using Practice.Common.Const;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace PracticeApi.Extensions.Base
{
    /// <summary>
    /// Identity 扩展类
    /// </summary>
    public static class IdentityExtension
    {
        /// <summary>
        /// 获取Identity中的信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        private static T GetIdentityValue<T>(this IIdentity identity, string claimType)
            where T : IConvertible
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(claimType);

            if (!string.IsNullOrWhiteSpace(claim?.Value))
                return (T)Convert.ChangeType(claim.Value, typeof(T), CultureInfo.InvariantCulture);

            return default;
        }

        /// <summary>
        /// 获取 userId
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetUserId<T>(this IIdentity identity)
            where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            return identity.GetIdentityValue<T>(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetRole<T>(this IIdentity identity)
            where T : IConvertible
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return identity.GetIdentityValue<T>(ClaimTypes.Role);
        }

        /// <summary>
        /// 获取昵称
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetNickName<T>(this IIdentity identity)
            where T : IConvertible
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            return identity.GetIdentityValue<T>(ClaimTypesExConst.NickName);
        }
    }
}
