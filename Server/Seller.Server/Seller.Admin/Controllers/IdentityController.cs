namespace Seller.Admin.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Identity;
    using Services.Identity;
    using Services;
    using static Seller.Shared.Infrastructure.InfrastructureConstants;

    public class IdentityController : AdministrationController
    {
        private readonly IIdentityService identityService;

        public IdentityController(
            IIdentityService identityService
           )
        {
            this.identityService = identityService;
          
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserInputModel model)
            => await this.Handle(
                async () =>
                {
                    var result = await this.identityService
                        .Login(model);

                    TokenService.Token = result.Token; // TODO this.Response.Cookies NOT WORKING NOT WORKING IN DOCKER

                    this.Response.Cookies.Append(
                        AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
                success: RedirectToAction(nameof(MessageController.Index), "Message"),
                failure: View("../Home/Index", model));

        [Authorize(Roles = "Admin")]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(AuthenticationCookieName);
            TokenService.Token = null; // TODO this.Response.Cookies NOT WORKING IN DOCKER
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
