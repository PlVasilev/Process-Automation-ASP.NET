namespace Seller.Identity.Features.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Seller.Identity.Data.Models;
    using Models;
    using Services.Interfaces;
    using Shared;
    using Shared.Controllers;
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;

        public IdentityController(
            UserManager<User> userManager, 
            IOptions<AppSettings> appSettings,
            IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult<UserOutputModel>> Register(UserInputModel model)
        {
            var user = await this.identityService.Register( model.UserName, model.Password);

            if (user == null) return BadRequest();

            return await Login(model);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<UserOutputModel>> Login(UserInputModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null) return Unauthorized();
            
            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid) return Unauthorized();

            var roles = await this.userManager.GetRolesAsync(user);

            return new UserOutputModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Token = identityService.GenerateJwtToken(user.Id,user.UserName,this.appSettings.Secret, roles)
            };
        }
    }
}
