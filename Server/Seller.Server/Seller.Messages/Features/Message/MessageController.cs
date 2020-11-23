namespace Seller.Messages.Features.Message
{
    using Microsoft.AspNetCore.Authorization;
    using Shared.Controllers;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Interfaces;


    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Add))]
        public async Task<ActionResult<bool>> Add(MessageRequestModel model) =>
            await messageService.Add(model.SenderId, model.SenderUsername, model.Title, model.Content);

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route(nameof(All))]
        public async Task<ActionResult<List<MessageResponseModel>>> All() =>
            await messageService.All();

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Process/{id}")]
        public async Task<ActionResult<bool>> Process(string id) =>
            await messageService.Process(id);
    }
}
