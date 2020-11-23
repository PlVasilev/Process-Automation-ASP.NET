namespace Seller.Listing.Gateway.Models.Deals
{
    public class DealResponseModelWithIsSale
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string CreatedOn { get; set; }

        public bool IsSale { get; set; }
    }
}
