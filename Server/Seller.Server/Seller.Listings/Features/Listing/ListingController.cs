namespace Seller.Listings.Features.Listing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Interfaces;
    using Listings.Features.Seller.Services.Interfaces;
    using Shared.Controllers;
    using Shared.Services.Identity;

    [Authorize]
    public class ListingController : ApiController
    {

        private readonly IListingService listingService;
        private readonly ICurrentUserService currentUser;
        private readonly ISellerService sellerService;

        public ListingController(IListingService listingService, ICurrentUserService currentUser, ISellerService sellerService)
        {
            this.listingService = listingService;
            this.currentUser = currentUser;
            this.sellerService = sellerService;
        }

 
        [HttpGet]
        [Route(nameof(All))]
        public async Task<IEnumerable<ListingAllResponseModel>> All() => await listingService.All();

        [HttpGet]
        [Route(nameof(Mine))]
        public async Task<IEnumerable<ListingAllResponseModel>> Mine() => await listingService.Mine(UserId());

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ListingDetailsResponseModel>> Details(string id) => await listingService.Details(id);

        [HttpGet]
        [Route("GetTitleAndSellerName/{id}")]
        public async Task<ActionResult<ListingTitleAndSellerNameResponseModel>> GetTitleAndSellerName(string id) => await listingService.GetTitleAndSellerName(id);

        [HttpPut]
        [Route(nameof(Update))]
        public async Task<ActionResult> Update(ListingUpdateRequestModel model)
        {
            var updated = await listingService.Update(model.Id, model.Title, model.Description, model.ImageUrl,
                model.Price, UserId());

            if (!updated) return BadRequest();
            
            return Ok();
        }

        [HttpPut]
        [Route(nameof(Deal))]
        public async Task<ActionResult<bool>> Deal(string id) => await listingService.Deal(id);

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await listingService.Delete(id, UserId());

            if (!deleted) return BadRequest();
            
            return Ok();
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult<ListingCreateResponseModel>> Create(ListingCreateRequestModel model) => await 
            listingService.Create(model.Title, model.Description, model.ImageUrl, model.Price, UserId());


        private string UserId() => sellerService.GetIdByUser(currentUser.UserId).Result.Id;

    }
}
