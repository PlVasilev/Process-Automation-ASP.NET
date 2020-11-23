namespace Seller.Offers.Messages
{
    using System.Threading.Tasks;
    using MassTransit;
    using Features.Offer.Services.Interfaces;
    using Seller.Shared.Messages.Offers;
    public class ListingDeletedConsumer : IConsumer<ListingDeletedMessage>
    {
        private readonly IOfferService offerService;


        public ListingDeletedConsumer(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        public async Task Consume(ConsumeContext<ListingDeletedMessage> context)
        {
            var message = context.Message.ListingId;
            await offerService.Delete(message);
        }
    }
}
