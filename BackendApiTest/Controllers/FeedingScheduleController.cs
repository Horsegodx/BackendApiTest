using BackendApiTest.Contracts.FeedingSchedule;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingScheduleController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public FeedingScheduleController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех расписаний кормления.
        /// </summary>
        /// <returns>Список расписаний в формате GetFeedingScheduleResponse.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var feedingSchedules = Context.FeedingSchedules.ToList();
            var response = feedingSchedules.Adapt<List<GetFeedingScheduleResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить расписание кормления по идентификатору.
        /// </summary>
        /// <param name="feedingId">Идентификатор кормления.</param>
        /// <param name="animalsId">Идентификатор животного.</param>
        /// <returns>Расписание в формате GetFeedingScheduleResponse или ошибка 400, если не найдено.</returns>
        [HttpGet("{feedingId}/{animalsId}")]
        public IActionResult GetById(int feedingId, int animalsId)
        {
            var feedingSchedule = Context.FeedingSchedules
                .Where(x => x.FeedingId == feedingId && x.AnimalsId == animalsId)
                .FirstOrDefault();

            if (feedingSchedule == null)
            {
                return BadRequest("Not Found");
            }

            var response = feedingSchedule.Adapt<GetFeedingScheduleResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Добавить новое расписание кормления.
        /// </summary>
        /// <param name="request">Данные для создания расписания в формате CreateFeedingScheduleRequest.</param>
        /// <returns>Созданное расписание в формате GetFeedingScheduleResponse.</returns>
        [HttpPost]
        public IActionResult Add(CreateFeedingScheduleRequest request)
        {
            var feedingSchedule = request.Adapt<FeedingSchedule>();
            Context.FeedingSchedules.Add(feedingSchedule);
            Context.SaveChanges();
            return Ok(feedingSchedule.Adapt<GetFeedingScheduleResponse>());
        }

        /// <summary>
        /// Обновить расписание кормления.
        /// </summary>
        /// <param name="request">Данные для обновления расписания в формате CreateFeedingScheduleRequest.</param>
        /// <param name="feedingId">Идентификатор кормления.</param>
        /// <param name="animalsId">Идентификатор животного.</param>
        /// <returns>Обновленное расписание в формате GetFeedingScheduleResponse или ошибка 400, если не найдено.</returns>
        [HttpPut("{feedingId}/{animalsId}")]
        public IActionResult Update(CreateFeedingScheduleRequest request, int feedingId, int animalsId)
        {
            var existingFeedingSchedule = Context.FeedingSchedules
                .Where(x => x.FeedingId == feedingId && x.AnimalsId == animalsId)
                .FirstOrDefault();

            if (existingFeedingSchedule == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingFeedingSchedule);
            Context.SaveChanges();
            return Ok(existingFeedingSchedule.Adapt<GetFeedingScheduleResponse>());
        }

        /// <summary>
        /// Удалить расписание кормления.
        /// </summary>
        /// <param name="feedingId">Идентификатор кормления.</param>
        /// <param name="animalsId">Идентификатор животного.</param>
        /// <returns>Сообщение об успешном удалении или ошибка 400, если не найдено.</returns>
        [HttpDelete("{feedingId}/{animalsId}")]
        public IActionResult Delete(int feedingId, int animalsId)
        {
            var feedingSchedule = Context.FeedingSchedules
                .Where(x => x.FeedingId == feedingId && x.AnimalsId == animalsId)
                .FirstOrDefault();

            if (feedingSchedule == null)
            {
                return BadRequest("Not Found");
            }

            Context.FeedingSchedules.Remove(feedingSchedule);
            Context.SaveChanges();

            // Возвращаем строку с подтверждением удаления
            return Ok("Feeding schedule successfully deleted");
        }
    }
}
