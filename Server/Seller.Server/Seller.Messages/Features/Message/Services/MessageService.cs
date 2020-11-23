namespace Seller.Messages.Features.Message.Services
{
    using Interfaces;
    using System;
    using System.Threading.Tasks;
    using Data;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Models;


    public class MessageService : IMessageService
    {
        private readonly MessageDbContext context;

        public MessageService(MessageDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Add(string senderId, string SenderUsername, string title, string content)
        {
            var message = new Data.Models.Message
            {
                Id = Guid.NewGuid().ToString(),
                SenderId = senderId,
                SenderUsername = SenderUsername,
                Created = DateTime.UtcNow,
                Title = title,
                Content = content,
                IsProcessed = false,
                IsDeleted = false
            };
            context.Add(message);
            var result = await context.SaveChangesAsync();
            return result != 0;
        }

        public async Task<List<MessageResponseModel>> All() => await
            context
                .Messages
                .Where(x => x.IsProcessed == false && x.IsDeleted == false)
                .Select(x => new MessageResponseModel
                {
                    SenderUsername = x.SenderUsername,
                    Content = x.Content,
                    Id = x.Id,
                    Title = x.Title,
                    Created = x.Created.ToString("g")
                }).ToListAsync();

        public async Task<bool> Process(string id)
        {
            var message = await context.Messages.FirstOrDefaultAsync(x => x.Id == id && x.IsProcessed == false && x.IsDeleted == false);

            message.IsProcessed = true;

           var result =  await context.SaveChangesAsync();

           return result != 0;
        }
            
    }
}
