namespace Seller.Messages.Features.Message.Models
{
    public class MessageRequestModel
    {
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }

        public string Title { get; set; }
   
        public string Content { get; set; }
    }
}
