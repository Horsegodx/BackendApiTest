using BackendApiTest.Contracts.Fertilization;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FertilizationController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public FertilizationController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех удобрений.
        /// </summary>
        /// <returns>Список удобрений в формате GetFertilizationResponse.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var fertilizations = Context.Fertilizations.ToList();
            var response = fertilizations.Adapt<List<GetFertilizationResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить удобрение по идентификатору.
        /// </summary>
        /// <param name="fertilizationId">Идентификатор удобрения.</param>
        /// <param name="plantId">Идентификатор растения.</param>
        /// <returns>Удобрение в формате GetFertilizationResponse или ошибка 400, если не найдено.</returns>
        [HttpGet("{fertilizationId}/{plantId}")]
        public IActionResult GetById(int fertilizationId, int plantId)
        {
            var fertilization = Context.Fertilizations
                .Where(x => x.FertilizationId == fertilizationId && x.PlantId == plantId)
                .FirstOrDefault();

            if (fertilization == null)
            {
                return BadRequest("Not Found");
            }

            var response = fertilization.Adapt<GetFertilizationResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Добавить новое удобрение.
        /// </summary>
        /// <param name="request">Данные для создания удобрения в формате CreateFertilizationRequest.</param>
        /// <returns>Созданное удобрение в формате GetFertilizationResponse.</returns>
        [HttpPost]
        public IActionResult Add(CreateFertilizationRequest request)
        {
            var fertilization = request.Adapt<Fertilization>();
            Context.Fertilizations.Add(fertilization);
            Context.SaveChanges();
            return Ok(fertilization.Adapt<GetFertilizationResponse>());
        }

        /// <summary>
        /// Обновить удобрение.
        /// </summary>
        /// <param name="request">Данные для обновления удобрения в формате CreateFertilizationRequest.</param>
        /// <param name="fertilizationId">Идентификатор удобрения.</param>
        /// <param name="plantId">Идентификатор растения.</param>
        /// <returns>Обновленное удобрение в формате GetFertilizationResponse или ошибка 400, если не найдено.</returns>
        [HttpPut("{fertilizationId}/{plantId}")]
        public IActionResult Update(CreateFertilizationRequest request, int fertilizationId, int plantId)
        {
            var existingFertilization = Context.Fertilizations
                .Where(x => x.FertilizationId == fertilizationId && x.PlantId == plantId)
                .FirstOrDefault();

            if (existingFertilization == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingFertilization);
            Context.SaveChanges();
            return Ok(existingFertilization.Adapt<GetFertilizationResponse>());
        }

        /// <summary>
        /// Удалить удобрение.
        /// </summary>
        /// <param name="fertilizationId">Идентификатор удобрения.</param>
        /// <param name="plantId">Идентификатор растения.</param>
        /// <returns>Сообщение об успешном удалении или ошибка 400, если не найдено.</returns>
        [HttpDelete("{fertilizationId}/{plantId}")]
        public IActionResult Delete(int fertilizationId, int plantId)
        {
            var fertilization = Context.Fertilizations
                .Where(x => x.FertilizationId == fertilizationId && x.PlantId == plantId)
                .FirstOrDefault();

            if (fertilization == null)
            {
                return BadRequest("Fertilization not found");  // Лучше вернуть более конкретное сообщение
            }

            Context.Fertilizations.Remove(fertilization);
            Context.SaveChanges();

            return Ok("Fertilization successfully deleted");  // Возвращаем подтверждающее сообщение
        }
    }
}
