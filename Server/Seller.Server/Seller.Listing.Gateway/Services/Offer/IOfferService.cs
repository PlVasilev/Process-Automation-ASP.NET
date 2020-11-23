namespace Seller.Listing.Gateway.Services.Offer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Refit;
    using Models.Offers;

    public interface IOfferService
    {
        [Get("/Offer/All/{id}")]
        Task<List<OfferResponceModel>> All(string id);

        [Put("/Offer/Accept")]
        Task<bool> Accept(string id);
    }
}
