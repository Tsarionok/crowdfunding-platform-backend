using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Service.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly SymmetricSecurityKey _key;

        public JwtService()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
        }

        public string CreateToken(string userName)
        {
            IList<Claim> claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, userName) };

            SigningCredentials credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
