using BackendApiTest.Contracts.WateringSchedule;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WateringScheduleController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public WateringScheduleController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить все расписания полива.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var schedules = Context.WateringSchedules
                .ProjectToType<GetWateringScheduleResponse>()
                .ToList();
            return Ok(schedules);
        }

        /// <summary>
        /// Получить расписание полива по идентификатору расписания полива и идентификатору растения.
        /// </summary>
        [HttpGet("{wateringScheduleId}/{plantId}")]
        public IActionResult GetById(int wateringScheduleId, int plantId)
        {
            var schedule = Context.WateringSchedules
                .Where(x => x.WateringScheduleId == wateringScheduleId && x.PlantId == plantId)
                .ProjectToType<GetWateringScheduleResponse>()
                .FirstOrDefault();

            if (schedule == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(schedule);
        }

        
        /// <summary>
        /// Добавить новое расписание полива.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreateWateringScheduleRequest request)
        {
            var schedule = request.Adapt<WateringSchedule>();
            Context.WateringSchedules.Add(schedule);
            Context.SaveChanges();

            var response = schedule.Adapt<GetWateringScheduleResponse>();
            return Ok(response); // Возвращаем добавленное расписание
        }

        /// <summary>
        /// Обновить существующее расписание полива.
        /// </summary>
        [HttpPut("{wateringScheduleId}/{plantId}")]
        public IActionResult Update(CreateWateringScheduleRequest request, int wateringScheduleId, int plantId)
        {
            var existingSchedule = Context.WateringSchedules
                .FirstOrDefault(x => x.WateringScheduleId == wateringScheduleId && x.PlantId == plantId);

            if (existingSchedule == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingSchedule);
            Context.SaveChanges();

            var response = existingSchedule.Adapt<GetWateringScheduleResponse>();
            return Ok(response); // Возвращаем обновленное расписание
        }

        /// <summary>
        /// Удалить расписание полива.
        /// </summary>
        [HttpDelete("{wateringScheduleId}/{plantId}")]
        public IActionResult Delete(int wateringScheduleId, int plantId)
        {
            var schedule = Context.WateringSchedules
                .FirstOrDefault(x => x.WateringScheduleId == wateringScheduleId && x.PlantId == plantId);

            if (schedule == null)
            {
                return BadRequest("Not Found");
            }

            Context.WateringSchedules.Remove(schedule);
            Context.SaveChanges();

            return Ok(); // Возвращаем успешный результат
        }
    }
}
