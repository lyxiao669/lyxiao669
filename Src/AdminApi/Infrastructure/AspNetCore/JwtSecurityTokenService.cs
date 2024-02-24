using Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DefaultJwtBearerOptions
    {
        public string Issuer { get; internal set; } = "juzhen";
        public string Audience { get; internal set; } = "juzhen";
        public string SigningKey { get; internal set; } = "Q05XSFQuH7v#tYlNS*SgNLR5vVdgt2pU";
    }
    public interface IJwtSecurityTokenService
    {
        TokenResponse CreateToken(List<Claim> claims, int expreIn);
    }
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
    public class JwtSecurityTokenService : IJwtSecurityTokenService
    {
        readonly DefaultJwtBearerOptions _options;

        public JwtSecurityTokenService()
        {
            _options = new DefaultJwtBearerOptions();
        }

        public TokenResponse CreateToken(List<Claim> claims, int expreIn)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_options.SigningKey));

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims.ToArray(),
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(expreIn),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponse
            {
                AccessToken = accessToken,
                ExpiresIn = expreIn,
                TokenType = "Bearer"
            };
        }
    }
}
