namespace Seller.Listing.Gateway.Controllers
{
    using Services.Deal;
    using Seller.Shared.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Deals;
    public class DealController : ApiController
    {
        private readonly IDealService dealService;
        public DealController(IDealService dealService)
        {

            this.dealService = dealService;
        }

        [HttpGet]
        [Authorize]
        [Route("Mine/{id}")]
        public async Task<List<DealResponseModelWithIsSale>> Mine(string id)
        {
            var buyDeals = await dealService.BuyDeals(id);
            var saleDeals = await dealService.SaleDeals(id);

            var result = buyDeals.Select(deal => new DealResponseModelWithIsSale
                {
                    Id = deal.Id,
                    CreatedOn = deal.CreatedOn,
                    Price = deal.Price,
                    Title = deal.Title,
                    IsSale = false
                })
                .ToList();

            result.AddRange(saleDeals.Select(deal => new DealResponseModelWithIsSale
            {
                Id = deal.Id,
                CreatedOn = deal.CreatedOn,
                Price = deal.Price,
                Title = deal.Title,
                IsSale = true
            }));

            return result;
        }
    }
}
