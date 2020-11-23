namespace Seller.Listings.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Listing
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Description { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public string DealId { get; set; }
        public Deal Deal { get; set; } 

        [Required]
        public string SellerId { get; set; }

        public UserSeller Seller { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsDeal { get; set; }

    }
}
