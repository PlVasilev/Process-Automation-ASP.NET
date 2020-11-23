namespace Seller.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Message;
    public class MessageController : AdministrationController
    {
        private readonly IMessageService message;

        public MessageController(IMessageService message)
            => this.message = message;

        public async Task<IActionResult> Index()
            => View(await this.message.All());


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Process(string id)
        {
           await this.message.Process(id);
           return RedirectToAction(nameof(HomeController.Index), "Home");
        }
       
    }
}
