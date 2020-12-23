using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Linq;

namespace PracticeApi.Extensions.Cors
{
    /// <summary>
    /// extenion method for CorsPolicy
    /// </summary>
    public static class CorsPolicyExtensions
    {
        /// <summary>
        /// Sets the <see cref="CorsPolicy.IsOriginAllowed"/> property of the policy to be a function
        /// that allows origins to match a configured wildcarded domain when evaluating if the 
        /// origin is allowed.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="allowLocalhost">if allow any origin from localhost with any port or without port</param>
        /// <returns></returns>
        public static CorsPolicy AllowedToAllowWildcardSubdomains(this CorsPolicy policy, bool allowLocalhost = false)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }
            if (allowLocalhost)
            {
                policy.IsOriginAllowed = policy.IsOriginAnAllowedSubdomainAndLocalhost;
            }
            else
            {
                policy.IsOriginAllowed = policy.IsOriginAnAllowedSubdomain;
            }
            return policy;
        }

        private const string WildcardSubdomain = "*.";

        private static bool IsOriginAnAllowedSubdomainAndLocalhost(this CorsPolicy policy, string origin)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }
            if (policy.Origins.Contains(origin))
            {
                return true;
            }

            if (Uri.TryCreate(origin, UriKind.Absolute, out var originUri))
            {
                return policy.Origins
                    .Where(o => o.Contains($"://{WildcardSubdomain}", StringComparison.Ordinal))
                    .Select(CreateDomainUri)
                    .Any(domain => IsSubdomainOf(originUri, domain))
                    || string.Equals(originUri.Host, "localhost", StringComparison.Ordinal);
            }

            return false;
        }

        private static bool IsOriginAnAllowedSubdomain(this CorsPolicy policy, string origin)
        {
            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }
            if (policy.Origins.Contains(origin))
            {
                return true;
            }

            if (Uri.TryCreate(origin, UriKind.Absolute, out var originUri))
            {
                return policy.Origins
                    .Where(o => o.Contains($"://{WildcardSubdomain}", StringComparison.Ordinal))
                    .Select(CreateDomainUri)
                    .Any(domain => IsSubdomainOf(originUri, domain));
            }

            return false;
        }

        private static Uri CreateDomainUri(string origin) => new Uri(origin.Replace(WildcardSubdomain, string.Empty, StringComparison.Ordinal), UriKind.Absolute);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool IsSubdomainOf(this Uri subdomain, Uri domain)
        {
            if (subdomain == null)
            {
                throw new ArgumentNullException(nameof(subdomain));
            }
            if (domain == null)
            {
                throw new ArgumentNullException(nameof(domain));
            }
            return subdomain.IsAbsoluteUri && domain.IsAbsoluteUri && subdomain.Scheme == domain.Scheme && subdomain.Port == domain.Port && subdomain.Host.EndsWith("." + domain.Host, StringComparison.Ordinal);
        }
    }
}
