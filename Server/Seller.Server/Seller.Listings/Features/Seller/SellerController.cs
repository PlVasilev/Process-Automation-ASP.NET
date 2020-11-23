namespace Seller.Listings.Features.Seller
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Interfaces;
    using Listings.Features.Seller.Services.Models;
    using Shared.Controllers;
    using Shared.Services.Identity;

    [Authorize]
    public class SellerController : ApiController
    {
        private readonly ISellerService sellerService;
        private readonly ICurrentUserService currentUser;

        public SellerController(ISellerService sellerService, ICurrentUserService currentUser)
        {
            this.sellerService = sellerService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Route("Id")]
        public async Task<ActionResult<SellerIdResponseModel>> GetSellerId() => await sellerService.GetIdByUser(currentUser.UserId);



        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(RegisterSellerRequestModel model)
        {
            var result = 
                await sellerService.CreateUserSeller(model.UserName, model.FirstName, model.LastName, model.Email, model.PhoneNumber, currentUser.UserId);

            if (result) return Ok();

            return BadRequest();
        }

    }
}
