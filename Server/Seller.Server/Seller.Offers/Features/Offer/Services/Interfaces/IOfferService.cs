namespace Seller.Offers.Features.Offer.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    public interface IOfferService
    {
        Task<OfferResponceModel> Add(decimal price, string creatorId, string listingId, string title, string creatorName);
        Task<List<OfferResponceModel>> All(string listingId);
        Task<List<OfferResponceModel>> Mine(string userId);
        Task<bool> Accept(string id);
        Task<bool> Delete(string listingId);
        Task<bool> DeleteOffer(string id);
        Task<bool> Edit(string listingId, string title);
        Task<int> GetOffersCount(string id);
        Task<decimal> GetCurrentOffer(string creatorId, string listingId);
    }
}
