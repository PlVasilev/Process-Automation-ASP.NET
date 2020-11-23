namespace Seller.Listings.Features.Seller.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterSellerRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }


    }
}

