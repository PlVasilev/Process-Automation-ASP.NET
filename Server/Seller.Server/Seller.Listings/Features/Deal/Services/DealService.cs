namespace Seller.Listings.Features.Deal.Services
{
    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using Models;
    using Data.Models;
    using Data;
    using System.Collections.Generic;
    using System.Linq;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Shared.Data.Models;
    using Shared.Messages.Offers;
    using Shared.Services;

    public class DealService : DataService<Listing>, IDealService
    {
        private readonly ListingsDbContext context;
        private readonly IBus publisher;

        public DealService(ListingsDbContext context, IBus publisher) : base(context)
        {
            this.context = context;
            this.publisher = publisher;
        }

        public async Task<bool> Create(DealCreateRequestModel model)
        {
            var deal = new Deal
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                BuyerId = model.BuyerId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                ListingId = model.ListingId,
                Price = model.Price,
                SellerId = model.SellerId
            };

            var listing = await this.context.Listings.FirstOrDefaultAsync(x => x.Id == model.ListingId);
            listing.IsDeal = true;

            context.Add(deal);
           // context.Listings.Update(listing);

            //var result = await context.SaveChangesAsync();

            var messageData = new ListingAcceptedMessage()
            {
                ListingId = model.OfferId
            };

            var messageId = Guid.NewGuid().ToString();
            var message = new Message(messageData, messageId);

            await this.Save(listing, message);
            await this.publisher.Publish(messageData);
            
            return true;
        }

        public async Task<List<DealResponseModel>> BuyDeals(string id) => await context.Deals
            .Where(x => x.BuyerId == id)
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => new DealResponseModel
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn.ToString("g"),
                Price = x.Price,
                Title = x.Title
            }).ToListAsync();

        public async Task<List<DealResponseModel>> SaleDeals(string id) => await context.Deals
            .Where(x => x.SellerId == id)
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => new DealResponseModel
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn.ToString("g"),
                Price = x.Price,
                Title = x.Title
            }).ToListAsync();
    }
}
