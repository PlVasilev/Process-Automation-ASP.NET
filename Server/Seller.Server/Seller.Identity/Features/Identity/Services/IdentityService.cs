namespace Seller.Identity.Features.Identity.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;
    using Data;
    using Seller.Identity.Data.Models;
    using Interfaces;
    using Shared;

    public class IdentityService : IIdentityService
    {
        private const string InvalidErrorMessage = "Invalid credentials.";

        private readonly UserManager<User> userManager;
        private readonly AppSettings appSettings;


        public IdentityService(UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

      

        public async Task<User> Register(string username, string password)
        {
            var user = new User()
            {
                UserName = username
            };

            await this.userManager.CreateAsync(user, password);

            if (userManager.Users.Count() == 1)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            await userManager.AddToRoleAsync(user, "User");

            return user;
        }
        public string GenerateJwtToken(string userId, string userName, string secret, IEnumerable<string> roles = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            };

            if (roles != null)
            {
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

 
    }
}
