using BackendApiTest.Contracts.PollAnswer;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollAnswerController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public PollAnswerController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех ответов на опросы.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var answers = Context.PollAnswers.ToList();
            var response = answers.Adapt<List<GetPollAnswerResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить ответ на опрос по идентификаторам.
        /// </summary>
        [HttpGet("{answerId}/{userId}/{optionId}/{pollId}")]
        public IActionResult GetById(int answerId, int userId, int optionId, int pollId)
        {
            var answer = Context.PollAnswers
                .FirstOrDefault(x => x.AnswerId == answerId && x.UserId == userId && x.OptionId == optionId);

            if (answer == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(answer.Adapt<GetPollAnswerResponse>());
        }

        /// <summary>
        /// Добавить новый ответ на опрос.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreatePollAnswerRequest request)
        {
            var answer = request.Adapt<PollAnswer>();
            Context.PollAnswers.Add(answer);
            Context.SaveChanges();
            return Ok(answer.Adapt<GetPollAnswerResponse>());
        }

        /// <summary>
        /// Обновить ответ на опрос.
        /// </summary>
        [HttpPut("{answerId}/{userId}/{optionId}/{pollId}")]
        public IActionResult Update(CreatePollAnswerRequest request, int answerId, int userId, int optionId, int pollId)
        {
            var existingAnswer = Context.PollAnswers
                .FirstOrDefault(x => x.AnswerId == answerId && x.UserId == userId && x.OptionId == optionId);

            if (existingAnswer == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingAnswer);
            Context.SaveChanges();
            return Ok(existingAnswer.Adapt<GetPollAnswerResponse>());
        }

        /// <summary>
        /// Удалить ответ на опрос.
        /// </summary>
        [HttpDelete("{answerId}/{userId}/{optionId}/{pollId}")]
        public IActionResult Delete(int answerId, int userId, int optionId, int pollId)
        {
            var answer = Context.PollAnswers
                .FirstOrDefault(x => x.AnswerId == answerId && x.UserId == userId && x.OptionId == optionId);

            if (answer == null)
            {
                return BadRequest("Not Found");
            }

            Context.PollAnswers.Remove(answer);
            Context.SaveChanges();
            return Ok();
        }
    }
}
