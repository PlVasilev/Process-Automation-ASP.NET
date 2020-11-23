namespace Seller.Messages.Features.Message.Models
{
    public class MessageResponseModel
    {
        public string Id { get; set; }
        public string SenderUsername { get; set; }
        public string Created { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
