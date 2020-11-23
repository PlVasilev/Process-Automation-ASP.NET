namespace Seller.Listings.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class UserSeller 
    {

        [Key]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Deal> SaleDeals { get; set; } = new List<Deal>();
        public IEnumerable<Deal> BuyDeals { get; set; } = new List<Deal>();
        public IEnumerable<Listing> Listings { get; set; } = new List<Listing>();
    }
}