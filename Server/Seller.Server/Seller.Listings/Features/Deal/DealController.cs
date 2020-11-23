namespace Seller.Listings.Features.Deal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Interfaces;
    using Shared.Controllers;

    [Authorize]
    public class DealController : ApiController
    {
        private readonly IDealService dealService;

        public DealController(IDealService dealService)
        {
            this.dealService = dealService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult<bool>> Create(DealCreateRequestModel model) => await
            dealService.Create(model);

        [HttpGet]
        [Route("BuyDeals/{id}")]
        public async Task<List<DealResponseModel>> BuyDeals(string id) => await
            dealService.BuyDeals(id);

        [HttpGet]
        [Route("SaleDeals/{id}")]
        public async Task<List<DealResponseModel>> SaleDeals(string id) => await
            dealService.SaleDeals(id);


    }
}
