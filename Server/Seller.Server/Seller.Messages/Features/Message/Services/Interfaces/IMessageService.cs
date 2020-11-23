namespace Seller.Messages.Features.Message.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    public interface IMessageService
    {
        Task<List<MessageResponseModel>> All();
        Task<bool> Process(string id);
        Task<bool> Add(string senderId, string SenderUsername, string title, string content);
    }
}
