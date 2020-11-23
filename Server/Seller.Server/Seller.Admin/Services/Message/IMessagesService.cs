namespace Seller.Admin.Services.Message
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Refit;
    using Seller.Admin.Models.Message;
    public interface IMessageService
    {
        [Get("/Message/All")]
        Task<List<MessageResponseModel>> All();

        [Get("/Message/Process/{id}")]
        Task<bool> Process(string id);
    }
}
