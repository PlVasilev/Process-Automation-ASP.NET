namespace Seller.Listing.Gateway.Services.Listing
{
    using System.Threading.Tasks;
    using Refit;
    using Models.Listings;
    public interface IListingService
    {
        [Get("/Listing/GetTitleAndSellerName/{id}")]
        Task<ListingTitleAndSellerNameResponseModel> GetTitleAndSellerName(string id);

    }
}
