namespace Seller.Listings.Features.Listing.Models
{
    public class ListingAllResponseModel
    {
  
        public string Id { get; set; }

        public string Title { get; set; }
      
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string Created { get; set; }
    }
}
