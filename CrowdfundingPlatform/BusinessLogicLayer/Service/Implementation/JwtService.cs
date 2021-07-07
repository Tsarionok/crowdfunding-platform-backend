using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Service.Implementation
{
    public class JwtService : BaseService, IJwtService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;

        public JwtService(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
            _userManager = userManager;
        }

        public string CreateToken(string email)
        {
            User user = _unitOfWork.Users.ReadByEmail(email).Result;
            IList<string> rolesList = _userManager.GetRolesAsync(user).Result;

            IList<Claim> claims = new List<Claim> { 
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            foreach(string role in rolesList)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }
            
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
