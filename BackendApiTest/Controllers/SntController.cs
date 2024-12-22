using BackendApiTest.Contracts.Animal;
using BackendApiTest.Contracts.Snt;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SntController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public SntController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить все СНТ.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var snts = Context.Snts
                .ProjectToType<GetSntResponse>()
                .ToList();
            return Ok(snts);
        }

        /// <summary>
        /// Получить СНТ по его идентификатору и идентификатору пользователя.
        /// </summary>
        [HttpGet("{sntId}/{userId}")]
        public IActionResult GetById(int sntId, int userId)
        {
            var snt = Context.Snts
                .Where(x => x.SntId == sntId && x.UserId == userId)
                .ProjectToType<GetSntResponse>()
                .FirstOrDefault();

            if (snt == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(snt);
        }

        /// <summary>
        /// Добавить новое СНТ.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreateSntRequest request)
        {
            var snt = request.Adapt<Snt>();
            Context.Snts.Add(snt);
            Context.SaveChanges();

            var response = snt.Adapt<GetSntResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Обновить существующее СНТ.
        /// </summary>
        [HttpPut("{sntId}/{userId}")]
        public IActionResult Update(CreateSntRequest request, int sntId, int userId)
        {
            var existingSnt = Context.Snts
                .Where(x => x.SntId == sntId && x.UserId == userId)
                .FirstOrDefault();

            if (existingSnt == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingSnt);
            Context.SaveChanges();

            var response = existingSnt.Adapt<GetSntResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Удалить СНТ.
        /// </summary>
        [HttpDelete("{sntId}/{userId}")]
        public IActionResult Delete(int sntId, int userId)
        {
            var snt = Context.Snts
                .Where(x => x.SntId == sntId && x.UserId == userId)
                .FirstOrDefault();

            if (snt == null)
            {
                return BadRequest("Not Found");
            }

            Context.Snts.Remove(snt);
            Context.SaveChanges();

            return Ok();
        }
    }
}
