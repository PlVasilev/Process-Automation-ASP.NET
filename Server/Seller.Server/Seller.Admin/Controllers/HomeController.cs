namespace Seller.Admin.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Seller.Admin.Models;
    using Seller.Shared.Infrastructure;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User.IsAdministrator())
            {
               return RedirectToAction(nameof(MessageController.Index), "Message");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            });
    }
}
