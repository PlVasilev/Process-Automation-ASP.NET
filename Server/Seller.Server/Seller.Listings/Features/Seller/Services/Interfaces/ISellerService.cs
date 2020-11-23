namespace Seller.Listings.Features.Seller.Services.Interfaces
{
    using System.Threading.Tasks;
    using Models;
    public interface ISellerService
    {
        Task<SellerIdResponseModel> GetIdByUser(string userId);
        Task<bool> CreateUserSeller(string userName, string firstName, string lastName, string email, string phoneNumber,string userId);
    }
}
