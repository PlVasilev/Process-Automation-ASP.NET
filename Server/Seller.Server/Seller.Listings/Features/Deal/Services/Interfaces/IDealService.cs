namespace Seller.Listings.Features.Deal.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IDealService
    {
        Task<bool> Create(DealCreateRequestModel model);
        Task<List<DealResponseModel>> BuyDeals(string id);
        Task<List<DealResponseModel>> SaleDeals(string id);
    }
}
