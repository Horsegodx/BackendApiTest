using BackendApiTest.Contracts.PollOption;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollOptionController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public PollOptionController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить все варианты ответов на опросы.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var options = Context.PollOptions
                .ProjectToType<GetPollOptionResponse>()
                .ToList();
            return Ok(options);
        }

        /// <summary>
        /// Получить вариант ответа на опрос по его идентификатору.
        /// </summary>
        [HttpGet("{optionId}/{pollId}")]
        public IActionResult GetById(int optionId, int pollId)
        {
            var option = Context.PollOptions
                .Where(x => x.OptionId == optionId && x.PollId == pollId)
                .ProjectToType<GetPollOptionResponse>()
                .FirstOrDefault();

            if (option == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(option);
        }

        /// <summary>
        /// Добавить новый вариант ответа на опрос.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreatePollOptionRequest request)
        {
            var option = request.Adapt<PollOption>();
            Context.PollOptions.Add(option);
            Context.SaveChanges();

            var response = option.Adapt<GetPollOptionResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Обновить существующий вариант ответа на опрос.
        /// </summary>
        [HttpPut]
        public IActionResult Update(CreatePollOptionRequest request, int optionId, int pollId)
        {
            var existingOption = Context.PollOptions
                .Where(x => x.OptionId == optionId && x.PollId == pollId)
                .FirstOrDefault();

            if (existingOption == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingOption);
            Context.SaveChanges();

            var response = existingOption.Adapt<GetPollOptionResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Удалить вариант ответа на опрос.
        /// </summary>
        [HttpDelete("{optionId}/{pollId}")]
        public IActionResult Delete(int optionId, int pollId)
        {
            var option = Context.PollOptions
                .Where(x => x.OptionId == optionId && x.PollId == pollId)
                .FirstOrDefault();

            if (option == null)
            {
                return BadRequest("Not Found");
            }

            Context.PollOptions.Remove(option);
            Context.SaveChanges();

            return Ok();
        }
    }
}
