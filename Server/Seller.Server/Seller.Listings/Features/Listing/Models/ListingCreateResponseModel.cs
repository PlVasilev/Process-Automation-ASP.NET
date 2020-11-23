namespace Seller.Listings.Features.Listing.Models
{

    using System;
    public class ListingCreateResponseModel
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime Created { get; set; }

        public string SellerId { get; set; }

        public bool IsDeleted { get; set; }

    }
}
