namespace Seller.Listings.Features
{
    using Shared.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Works");
        }
    }
}
