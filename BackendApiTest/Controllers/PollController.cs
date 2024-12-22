using BackendApiTest.Contracts.Poll;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public PollController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить все опросы.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var polls = Context.Polls
                .ProjectToType<GetPollResponse>()
                .ToList();
            return Ok(polls);
        }

        /// <summary>
        /// Получить опрос по его идентификатору.
        /// </summary>
        [HttpGet("{pollId}")]
        public IActionResult GetById(int pollId)
        {
            var poll = Context.Polls
                .Where(x => x.PollId == pollId)
                .ProjectToType<GetPollResponse>()
                .FirstOrDefault();

            if (poll == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(poll);
        }

        /// <summary>
        /// Добавить новый опрос.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreatePollRequest request)
        {
            var poll = request.Adapt<Poll>();
            Context.Polls.Add(poll);
            Context.SaveChanges();

            var response = poll.Adapt<GetPollResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Обновить существующий опрос.
        /// </summary>
        [HttpPut]
        public IActionResult Update(CreatePollRequest request, int pollId)
        {
            var existingPoll = Context.Polls
                .Where(x => x.PollId == pollId)
                .FirstOrDefault();

            if (existingPoll == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingPoll);
            Context.SaveChanges();

            var response = existingPoll.Adapt<GetPollResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Удалить опрос.
        /// </summary>
        [HttpDelete("{pollId}")]
        public IActionResult Delete(int pollId)
        {
            var poll = Context.Polls
                .Where(x => x.PollId == pollId)
                .FirstOrDefault();

            if (poll == null)
            {
                return BadRequest("Not Found");
            }

            Context.Polls.Remove(poll);
            Context.SaveChanges();

            return Ok();
        }
    }
}
