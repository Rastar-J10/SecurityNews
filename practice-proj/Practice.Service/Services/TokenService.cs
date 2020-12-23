using Autofac.AttributeExtensions.Attributes;
using Microsoft.IdentityModel.Tokens;
using Practice.Common.Const;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Practice.Services
{
    /// <summary>
    /// token业务类
    /// </summary>
    [InstancePerLifetimeScope]
    public class TokenService : ITokenService
    {
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string CreateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConst.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                TokenConst.Issuer,
                TokenConst.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddDays(30),
                creds
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return $"Bearer {tokenHandler.WriteToken(token)}";
        }
    }
}
