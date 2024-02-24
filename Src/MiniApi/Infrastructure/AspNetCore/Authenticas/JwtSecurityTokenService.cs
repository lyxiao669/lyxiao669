using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Juzhen.MiniProgramAPI.Infrastructure
{
    public class JwtSecurityTokenService
    {
        readonly MyJwtBearerOptions _jwtBearer;
       
        public JwtSecurityTokenService(MyJwtBearerOptions jwtBearer)
        {
            _jwtBearer = jwtBearer;
        }
        
        public string CreateToken(List<Claim>  claims)
        {
            var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtBearer.Key));
            var token = new JwtSecurityToken(
                issuer: _jwtBearer.Issuer,
                audience: _jwtBearer.Audience,
                claims: claims.ToArray(),
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
