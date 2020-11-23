namespace Seller.Listings.Features.Listing.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Shared.Services;
    using Data.Models;

    public interface IListingService : IDataService<Listing>
    {
        public Task<ListingCreateResponseModel> Create(string title, string description, string imageUrl, decimal price, string userId);

        public Task<ListingDetailsResponseModel> Details(string id);
        public Task<ListingTitleAndSellerNameResponseModel> GetTitleAndSellerName(string id);

        public Task<bool> Update(string id, string title, string description, string imageUrl, decimal price, string userId);

        public Task<bool> Delete(string id, string userId);

        public Task<bool> Deal(string id);

        public Task<IEnumerable<ListingAllResponseModel>> All();

        public Task<IEnumerable<ListingAllResponseModel>> Mine(string userId);

    }
}
