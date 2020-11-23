namespace Seller.Identity.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserInputModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
