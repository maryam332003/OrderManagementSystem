using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;



namespace OrdersManagement.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(User user, UserManager<User> _userManager)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),


            };
            var UserRoles = await _userManager.GetRolesAsync(user);

            foreach (var Role in UserRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, Role));

            }


			var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

			var Token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"], 

				audience: _configuration["JWT:ValidAudience"],
				
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ExpirationDay"])),
				
                claims: AuthClaims,
				
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature) );

			return new JwtSecurityTokenHandler().WriteToken(Token);
        
        }

    }
}
