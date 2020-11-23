namespace Seller.Admin.Models.Identity
{
    using System.ComponentModel.DataAnnotations;
    public class UserInputModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
