using BackendApiTest.Contracts.SntEvent;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SntEventController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public SntEventController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить все события СНТ.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var sntEvents = Context.SntEvents
                .ProjectToType<GetSntEventResponse>()
                .ToList();
            return Ok(sntEvents);
        }

        /// <summary>
        /// Получить событие по его идентификатору события, СНТ и пользователя.
        /// </summary>
        [HttpGet("{eventId}/{sntId}/{userId}")]
        public IActionResult GetById(int eventId, int sntId, int userId)
        {
            var sntEvent = Context.SntEvents
                .Where(x => x.EventId == eventId && x.SntId == sntId && x.UserId == userId)
                .ProjectToType<GetSntEventResponse>()
                .FirstOrDefault();

            if (sntEvent == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(sntEvent);
        }

        /// <summary>
        /// Добавить новое событие.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreateSntEventRequest request)
        {
            var sntEvent = request.Adapt<SntEvent>();
            Context.SntEvents.Add(sntEvent);
            Context.SaveChanges();

            var response = sntEvent.Adapt<GetSntEventResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Обновить существующее событие.
        /// </summary>
        [HttpPut("{eventId}/{sntId}/{userId}")]
        public IActionResult Update(CreateSntEventRequest request, int eventId, int sntId, int userId)
        {
            var existingSntEvent = Context.SntEvents
                .Where(x => x.EventId == eventId && x.SntId == sntId && x.UserId == userId)
                .FirstOrDefault();

            if (existingSntEvent == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingSntEvent);
            Context.SaveChanges();

            var response = existingSntEvent.Adapt<GetSntEventResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Удалить событие.
        /// </summary>
        [HttpDelete("{eventId}/{sntId}/{userId}")]
        public IActionResult Delete(int eventId, int sntId, int userId)
        {
            var sntEvent = Context.SntEvents
                .Where(x => x.EventId == eventId && x.SntId == sntId && x.UserId == userId)
                .FirstOrDefault();

            if (sntEvent == null)
            {
                return BadRequest("Not Found");
            }

            Context.SntEvents.Remove(sntEvent);
            Context.SaveChanges();

            return Ok();
        }
    }
}
