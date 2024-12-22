using BackendApiTest.Contracts.Animal;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public AnimalsController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех животных.
        /// </summary>
        /// <returns>Список животных в формате GetAnimalsResponse.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var animals = Context.Animals
                .ProjectToType<GetAnimalsResponse>() // Mapster автоматически преобразует сущности в DTO
                .ToList();

            return Ok(animals);
        }

        /// <summary>
        /// Получить информацию о конкретном животном по его идентификатору.
        /// </summary>
        /// <param name="animalsId">Идентификатор животного.</param>
        /// <returns>Информация о животном в формате GetAnimalsResponse.</returns>
        [HttpGet("{animalsId}")]
        public IActionResult GetById(int animalsId)
        {
            var animal = Context.Animals
                .Where(x => x.AnimalsId == animalsId)
                .ProjectToType<GetAnimalsResponse>()
                .FirstOrDefault();

            if (animal == null)
            {
                return NotFound("Animal not found");
            }

            return Ok(animal);
        }

        /// <summary>
        /// Добавить новое животное.
        /// </summary>
        /// <param name="model">Данные животного в формате CreateAnimalsRequest.</param>
        /// <returns>Добавленное животное в формате GetAnimalsResponse.</returns>
        [HttpPost]
        public IActionResult Add([FromBody] CreateAnimalsRequest model)
        {
            // Маппинг из CreateAnimalsRequest в сущность Animal
            var animal = model.Adapt<Animal>();

            Context.Animals.Add(animal);
            Context.SaveChanges();

            // Возвращаем результат в формате GetAnimalsResponse
            var response = animal.Adapt<GetAnimalsResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Обновить информацию о животном.
        /// </summary>
        /// <param name="animalsId">Идентификатор животного для обновления.</param>
        /// <param name="model">Новые данные животного в формате CreateAnimalsRequest.</param>
        /// <returns>Обновлённое животное в формате GetAnimalsResponse.</returns>
        [HttpPut("{animalsId}")]
        public IActionResult Update(int animalsId, [FromBody] CreateAnimalsRequest model)
        {
            var existingAnimal = Context.Animals.FirstOrDefault(x => x.AnimalsId == animalsId);

            if (existingAnimal == null)
            {
                return NotFound("Animal not found");
            }

            // Обновляем значения существующего животного
            model.Adapt(existingAnimal);
            Context.SaveChanges();

            var response = existingAnimal.Adapt<GetAnimalsResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Удалить животное по его идентификатору.
        /// </summary>
        /// <param name="animalsId">Идентификатор животного для удаления.</param>
        /// <returns>Сообщение об успешном удалении.</returns>
        [HttpDelete("{animalsId}")]
        public IActionResult Delete(int animalsId)
        {
            var animal = Context.Animals.FirstOrDefault(x => x.AnimalsId == animalsId);

            if (animal == null)
            {
                return NotFound("Animal not found");
            }

            Context.Animals.Remove(animal);
            Context.SaveChanges();

            return Ok("Animal successfully deleted");
        }
    }
}
