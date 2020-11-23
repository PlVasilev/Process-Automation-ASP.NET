namespace Seller.Listings.Features.Listing.Services
{
    using MassTransit;
    using Shared.Data.Models;
    using Shared.Messages.Offers;
    using Shared.Services;
    using System;
    using System.Threading.Tasks;
    using Data;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Data.Models;
    public class ListingService : DataService<Listing>, IListingService
    {
        private readonly ListingsDbContext context;
        private readonly IBus publisher;

        public ListingService(ListingsDbContext context, IBus publisher) : base(context)
        {
            this.context = context;
            this.publisher = publisher;
        }
        public async Task<ListingCreateResponseModel> Create(string title, string description, string imageUrl, decimal price, string userId)
        {

            var listing = new Listing()
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Created = DateTime.UtcNow,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                IsDeleted = false,
                SellerId = userId
            };

            var messageData = new ListingCreatedMessage
            {
                Title = listing.Title,
                Price = listing.Price
            };

            var id = Guid.NewGuid().ToString();
            var message = new Message(messageData, id);

            this.context.Add(message);

            this.context.Add(listing);

            await context.SaveChangesAsync();

            await this.publisher.Publish(messageData);

            await this.MarkMessageAsPublished(message.Id);

            return new ListingCreateResponseModel()
            {
                Id = listing.Id,
                Title = listing.Title,
                Created = listing.Created,
                Description = listing.Description,
                ImageUrl = listing.ImageUrl,
                Price = listing.Price,
                IsDeleted = listing.IsDeleted,
                SellerId = listing.SellerId
            };
        }

        public async Task<ListingDetailsResponseModel> Details(string id) => await this.context
            .Listings
            .Where(l => l.IsDeleted == false && l.IsDeal == false && l.Id == id)
            .Select(l => new ListingDetailsResponseModel()
            {
                Id = l.Id,
                Title = l.Title,
                ImageUrl = l.ImageUrl,
                Price = l.Price,
                Description = l.Description,
                OffersCount = 0,
                SellerId = l.SellerId,
                SellerName = l.Seller.FirstName + " " + l.Seller.LastName,
                Created = l.Created.ToString("D")
            }).FirstOrDefaultAsync();

        public async Task<ListingTitleAndSellerNameResponseModel> GetTitleAndSellerName(string id)
        {
            var result = await context
                .Listings
                .Include(x => x.Seller)
                .FirstOrDefaultAsync(l => l.IsDeleted == false && l.Id == id && l.IsDeal == false);

            return new ListingTitleAndSellerNameResponseModel
            {
                SellerName = result.Seller.LastName + " " + result.Seller.LastName,
                Title = result.Title
            };
        }

        public async Task<bool> Update(string id, string title, string description, string imageUrl, decimal price, string userId)
        {
            var listing = await this.context
                .Listings
                .Where(l => l.Id == id && l.SellerId == userId && l.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (listing == null) return false;

            var previousTitle = listing.Title;

            listing.Title = title;
            listing.Description = description;
            listing.ImageUrl = imageUrl;
            listing.Price = price;
            listing.Created = DateTime.UtcNow;

            var messageData = new ListingEditedMessage
            {
                ListingId = listing.Id,
                Title = title
            };

            var messageId = Guid.NewGuid().ToString();
            var message = new Message(messageData, messageId);

            await this.Save(listing, message);

            if (title != previousTitle)
            {
                await this.publisher.Publish(messageData);
            }

            await this.MarkMessageAsPublished(messageId);
            
            return true;
        }

        public async Task<bool> Delete(string id, string userId)
        {
            var listing = await this.context
                .Listings
                .Where(l => l.Id == id && l.SellerId == userId && l.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (listing == null) return false;

            listing.IsDeleted = true;
            var messageData = new ListingDeletedMessage()
            {
                ListingId = listing.Id
            };

            var messageId = Guid.NewGuid().ToString();
            var message = new Message(messageData, messageId);

            await this.Save(listing, message);
        
            await this.publisher.Publish(messageData);
            
            return true;
        }

        public async Task<bool> Deal(string id)
        {
            var listing = await context.Listings.FirstOrDefaultAsync(x => x.Id == id);

            listing.IsDeal = true;

            context.Update(listing);

            var result = await context.SaveChangesAsync();

            return result != 0;
        }


        public async Task<IEnumerable<ListingAllResponseModel>> All() => await this.context
            .Listings
            .Where(l => l.IsDeleted == false && l.IsDeal == false)
            .OrderByDescending(l => l.Created)
            .Select(l => new ListingAllResponseModel
            {
                Id = l.Id,
                Title = l.Title,
                ImageUrl = l.ImageUrl,
                Price = l.Price,
                Created = l.Created.ToString("D")
            }).ToListAsync();


        public async Task<IEnumerable<ListingAllResponseModel>> Mine(string userId) => await this.context
            .Listings
            .Where(l => l.SellerId == userId && l.IsDeleted == false && l.IsDeal == false)
            .OrderByDescending(l => l.Created)
            .Select(l => new ListingAllResponseModel
            {
                Id = l.Id,
                Title = l.Title,
                ImageUrl = l.ImageUrl,
                Price = l.Price,
                Created = l.Created.ToString("D")
            }).ToListAsync();


    }
}
