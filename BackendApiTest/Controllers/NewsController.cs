using BackendApiTest.Contracts.News;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public NewsController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех новостей.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var newsList = Context.News.ToList();
            var response = newsList.Adapt<List<GetNewsResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить новость по идентификаторам.
        /// </summary>
        [HttpGet("{newsId}/{userId}")]
        public IActionResult GetById(int newsId, int userId)
        {
            var news = Context.News
                .FirstOrDefault(x => x.NewsId == newsId && x.UserId == userId);

            if (news == null) return BadRequest("Not Found");

            return Ok(news.Adapt<GetNewsResponse>());
        }

        /// <summary>
        /// Добавить новость.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreateNewsRequest request)
        {
            var news = request.Adapt<News>();
            Context.News.Add(news);
            Context.SaveChanges();
            return Ok(news.Adapt<GetNewsResponse>());
        }

        /// <summary>
        /// Обновить новость.
        /// </summary>
        [HttpPut("{newsId}/{userId}")]
        public IActionResult Update(CreateNewsRequest request, int newsId, int userId)
        {
            var existingNews = Context.News
                .FirstOrDefault(x => x.NewsId == newsId && x.UserId == userId);

            if (existingNews == null) return BadRequest("Not Found");

            request.Adapt(existingNews);
            Context.SaveChanges();
            return Ok(existingNews.Adapt<GetNewsResponse>());
        }

        /// <summary>
        /// Удалить новость.
        /// </summary>
        [HttpDelete("{newsId}/{userId}")]
        public IActionResult Delete(int newsId, int userId)
        {
            var news = Context.News
                .FirstOrDefault(x => x.NewsId == newsId && x.UserId == userId);

            if (news == null) return BadRequest("Not Found");

            Context.News.Remove(news);
            Context.SaveChanges();
            return Ok();
        }
    }
}
