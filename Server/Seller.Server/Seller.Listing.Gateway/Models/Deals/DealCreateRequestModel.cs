namespace Seller.Listing.Gateway.Models.Deals
{
    public class DealCreateRequestModel
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ListingId { get; set; }
        public string OfferId { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
    }
}
