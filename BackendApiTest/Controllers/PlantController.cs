using BackendApiTest.Contracts.Plant;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public PlantController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех растений.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var plants = Context.Plants.ToList();
            var response = plants.Adapt<List<GetPlantResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить растение по идентификатору.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var plant = Context.Plants
                .FirstOrDefault(x => x.PlantId == id);

            if (plant == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(plant.Adapt<GetPlantResponse>());
        }

        /// <summary>
        /// Добавить новое растение.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreatePlantRequest request)
        {
            var plant = request.Adapt<Plant>();
            Context.Plants.Add(plant);
            Context.SaveChanges();
            return Ok(plant.Adapt<GetPlantResponse>());
        }

        /// <summary>
        /// Обновить информацию о растении.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(CreatePlantRequest request, int id)
        {
            var existingPlant = Context.Plants
                .FirstOrDefault(x => x.PlantId == id);

            if (existingPlant == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingPlant);
            Context.SaveChanges();
            return Ok(existingPlant.Adapt<GetPlantResponse>());
        }

        /// <summary>
        /// Удалить растение.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var plant = Context.Plants
                .FirstOrDefault(x => x.PlantId == id);

            if (plant == null)
            {
                return BadRequest("Not Found");
            }

            Context.Plants.Remove(plant);
            Context.SaveChanges();
            return Ok();
        }
    }
}
