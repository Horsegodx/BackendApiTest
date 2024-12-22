using BackendApiTest.Contracts.User;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseresController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public UseresController(CoutryhouseeContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Получить по идентификатору всех!!!!
        /// </summary>
        /// <param name="model">Пользователи</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = Context.Users.ToList();

            // Преобразование в DTO с использованием Mapster
            List<GetUserResponse> getUserResponse = users.Adapt<List<GetUserResponse>>();

            return Ok(getUserResponse);
        }

        /// <summary>
        /// Получить по идентификатору
        /// </summary>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();

            if (user == null)
            {
                return BadRequest("Not Found");
            }

            // Автоматическое преобразование в UserDto с помощью Mapster
            var userDto = user.Adapt<GetUserResponse>();

            return Ok(userDto);
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "login" : "123",
        ///        "password" : "12345",
        ///        "firstname" : "Иван",
        ///        "lastname" : "Иванов",
        ///        "middlename" : "Иванович"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Add(CreateUserRequest model)
        {
            // Маппинг из CreateUserRequest в сущность User
            var user = model.Adapt<User>();

            Context.Users.Add(user);
            Context.SaveChanges();

            // Маппинг обратно в GetUserResponse, чтобы вернуть только нужные данные
            var response = user.Adapt<GetUserResponse>();

            return Ok(response);
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(int id, CreateUserRequest model)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Обновление данных пользователя из CreateUserRequest
            model.Adapt(user);

            Context.Users.Update(user);
            Context.SaveChanges();

            // Возвращаем обновлённого пользователя с помощью GetUserResponse
            var response = user.Adapt<GetUserResponse>();

            return Ok(response);
        }

        /// <summary>
        /// Удаление пользователя 
        /// </summary>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            Context.Users.Remove(user);
            Context.SaveChanges();

            return Ok("User successfully deleted");
        }

    }
}
