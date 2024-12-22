using BackendApiTest.Contracts.Harvest;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HarvestController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public HarvestController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех урожаев.
        /// </summary>
        /// <returns>Список урожаев в формате GetHarvestResponse.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var harvests = Context.Harvests.ToList();
            var response = harvests.Adapt<List<GetHarvestResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить урожай по идентификатору.
        /// </summary>
        /// <param name="harvestId">Идентификатор урожая.</param>
        /// <param name="plantId">Идентификатор растения.</param>
        /// <returns>Урожай в формате GetHarvestResponse или ошибка 400, если не найдено.</returns>
        [HttpGet("{harvestId}/{plantId}")]
        public IActionResult GetById(int harvestId, int plantId)
        {
            var harvest = Context.Harvests
                .Where(x => x.HarvestId == harvestId && x.PlantId == plantId)
                .FirstOrDefault();

            if (harvest == null)
            {
                return BadRequest("Not Found");
            }

            var response = harvest.Adapt<GetHarvestResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Добавить новый урожай.
        /// </summary>
        /// <param name="request">Данные для создания урожая в формате CreateHarvestRequest.</param>
        /// <returns>Созданный урожай в формате GetHarvestResponse.</returns>
        [HttpPost]
        public IActionResult Add(CreateHarvestRequest request)
        {
            var harvest = request.Adapt<Harvest>();
            Context.Harvests.Add(harvest);
            Context.SaveChanges();
            return Ok(harvest.Adapt<GetHarvestResponse>());
        }

        /// <summary>
        /// Обновить данные об урожае.
        /// </summary>
        /// <param name="request">Данные для обновления урожая в формате CreateHarvestRequest.</param>
        /// <param name="harvestId">Идентификатор урожая.</param>
        /// <param name="plantId">Идентификатор растения.</param>
        /// <returns>Обновленный урожай в формате GetHarvestResponse или ошибка 400, если не найдено.</returns>
        [HttpPut("{harvestId}/{plantId}")]
        public IActionResult Update(CreateHarvestRequest request, int harvestId, int plantId)
        {
            var existingHarvest = Context.Harvests
                .Where(x => x.HarvestId == harvestId && x.PlantId == plantId)
                .FirstOrDefault();

            if (existingHarvest == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingHarvest);
            Context.SaveChanges();
            return Ok(existingHarvest.Adapt<GetHarvestResponse>());
        }

        /// <summary>
        /// Удалить урожай.
        /// </summary>
        /// <param name="harvestId">Идентификатор урожая.</param>
        /// <param name="plantId">Идентификатор растения.</param>
        /// <returns>Сообщение об успешном удалении или ошибка 400, если не найдено.</returns>
        [HttpDelete("{harvestId}/{plantId}")]
        public IActionResult Delete(int harvestId, int plantId)
        {
            var harvest = Context.Harvests
                .Where(x => x.HarvestId == harvestId && x.PlantId == plantId)
                .FirstOrDefault();

            if (harvest == null)
            {
                return BadRequest("Not Found");
            }

            Context.Harvests.Remove(harvest);
            Context.SaveChanges();
            return Ok("Harvest successfully deleted");  // Возвращаем строку подтверждения
        }
    }
}
