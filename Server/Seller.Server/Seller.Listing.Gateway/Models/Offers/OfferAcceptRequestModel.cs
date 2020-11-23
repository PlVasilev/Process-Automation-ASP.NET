namespace Seller.Listing.Gateway.Models.Offers
{
    public class OfferAcceptRequestModel
    {
        public string Id { get; set; }
        public string ListingId { get; set; }
        public string CreatorId { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string BuyerId { get; set; }
    }
}
