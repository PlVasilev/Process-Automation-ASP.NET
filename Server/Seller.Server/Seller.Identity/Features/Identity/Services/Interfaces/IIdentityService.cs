namespace Seller.Identity.Features.Identity.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Seller.Identity.Data.Models;
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret, IEnumerable<string> roles);
        Task<User> Register(string username, string password);
        //Task<bool> CreateUserSeller(string firstName, string lastName, string userId);
    }
}
