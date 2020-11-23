namespace Seller.Offers.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Offer
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ListingId { get; set; }

        [Required]
        public string Title { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string CreatorId { get; set; }
        [Required]
        public string CreatorName { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsDeleted { get; set; }

    }
}
