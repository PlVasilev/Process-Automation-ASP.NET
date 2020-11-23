namespace Seller.Listings.Features.Listing.Models
{
    public class ListingDetailsResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public int OffersCount { get; set; }
        public string SellerId { get; set; }
        public string SellerName { get; set; }
    }
}
