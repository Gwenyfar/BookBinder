
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Security
{
    public class JWTProvider : IJWTProvider
    {
        public string GetAccessToken(string email, Guid userId)
        {
            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, "bookbinderapi"),
                new Claim(JwtRegisteredClaimNames.Aud, "bookbinder.com")
            };
            var expiry = DateTime.UtcNow.AddMinutes(30);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bookbinder.com72984034875-234873647859"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var accessToken = new JwtSecurityToken(claims: userClaims, signingCredentials: signingCredentials, expires: expiry);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
    }
}
