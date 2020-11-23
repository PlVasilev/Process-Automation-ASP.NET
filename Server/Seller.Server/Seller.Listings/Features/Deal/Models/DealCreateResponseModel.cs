namespace Seller.Listings.Features.Deal.Models
{
    public class DealCreateResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ListingId { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public string CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
