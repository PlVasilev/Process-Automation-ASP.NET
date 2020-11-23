namespace Seller.Listing.Gateway.Services.Deal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Refit;
    using Models.Deals;
    public interface IDealService
    {
        [Post("/Deal/Create")]
        Task<bool> Create(DealCreateRequestModel model);

        [Get("/Deal/BuyDeals/{id}")]
        Task<List<DealResponseModel>> BuyDeals(string id);

        [Get("/Deal/SaleDeals/{id}")]
        Task<List<DealResponseModel>> SaleDeals(string id);
    }
}
