using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Kutian.Utilities.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kutian.Utilities.Core.Utils
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration) => _configuration = configuration;

        public string GetToken(string uniqueName, List<Claim> customClaims = null)
        {
            var now = DateTime.UtcNow;
            var nowDateTimeOffset = new DateTimeOffset(now);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName),
                new Claim(JwtRegisteredClaimNames.Sub, uniqueName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, nowDateTimeOffset.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };

            if (customClaims != null)
                claims.AddRange(customClaims);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration[$"{ nameof(AuthenticationSettings) }:{ nameof(AuthenticationSettings.JWTIssuer) }"],
                audience: _configuration[$"{ nameof(AuthenticationSettings) }:{ nameof(AuthenticationSettings.JWTAudience) }"],
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(
                    Convert.ToInt16(
                        _configuration[$"{ nameof(AuthenticationSettings) }:{ nameof(AuthenticationSettings.ExpiresInMinutes) }"]
                    )
                ),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            _configuration[$"{nameof(AuthenticationSettings)}:{nameof(AuthenticationSettings.JWTKey)}"])
                        ),
                        SecurityAlgorithms.HmacSha256
                    )
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}