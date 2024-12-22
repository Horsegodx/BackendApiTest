using BackendApiTest.Contracts.Message;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public MessageController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех сообщений.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = Context.Messages.ToList();
            var response = messages.Adapt<List<GetMessageResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить сообщение по идентификаторам.
        /// </summary>
        [HttpGet("{messageId}/{userId}")]
        public IActionResult GetById(int messageId, int userId)
        {
            var message = Context.Messages
                .FirstOrDefault(x => x.MessageId == messageId && x.UserId == userId);

            if (message == null) return BadRequest("Not Found");

            return Ok(message.Adapt<GetMessageResponse>());
        }

        /// <summary>
        /// Добавить новое сообщение.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreateMessageRequest request)
        {
            var message = request.Adapt<Message>();
            Context.Messages.Add(message);
            Context.SaveChanges();
            return Ok(message.Adapt<GetMessageResponse>());
        }

        /// <summary>
        /// Обновить сообщение.
        /// </summary>
        [HttpPut("{messageId}/{userId}")]
        public IActionResult Update(CreateMessageRequest request, int messageId, int userId)
        {
            var existingMessage = Context.Messages
                .FirstOrDefault(x => x.MessageId == messageId && x.UserId == userId);

            if (existingMessage == null) return BadRequest("Not Found");

            request.Adapt(existingMessage);
            Context.SaveChanges();
            return Ok(existingMessage.Adapt<GetMessageResponse>());
        }

        /// <summary>
        /// Удалить сообщение.
        /// </summary>
        [HttpDelete("{messageId}/{userId}")]
        public IActionResult Delete(int messageId, int userId)
        {
            var message = Context.Messages
                .FirstOrDefault(x => x.MessageId == messageId && x.UserId == userId);

            if (message == null) return BadRequest("Not Found");

            Context.Messages.Remove(message);
            Context.SaveChanges();
            return Ok();
        }
    }
}
