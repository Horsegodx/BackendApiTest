using BackendApiTest.Contracts.Shop;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public ShopController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить все магазины.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var shops = Context.Shops
                .ProjectToType<GetShopResponse>()
                .ToList();
            return Ok(shops);
        }

        /// <summary>
        /// Получить магазин по его идентификатору.
        /// </summary>
        [HttpGet("{shopId}")]
        public IActionResult GetById(int shopId)
        {
            var shop = Context.Shops
                .Where(x => x.ShopId == shopId)
                .ProjectToType<GetShopResponse>()
                .FirstOrDefault();

            if (shop == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(shop);
        }

        /// <summary>
        /// Добавить новый магазин.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreateShopRequest request)
        {
            var shop = request.Adapt<Shop>();
            Context.Shops.Add(shop);
            Context.SaveChanges();

            var response = shop.Adapt<GetShopResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Обновить информацию о магазине.
        /// </summary>
        [HttpPut("{shopId}")]
        public IActionResult Update(CreateShopRequest request, int shopId)
        {
            var existingShop = Context.Shops
                .Where(x => x.ShopId == shopId)
                .FirstOrDefault();

            if (existingShop == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingShop);
            Context.SaveChanges();

            var response = existingShop.Adapt<GetShopResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Удалить магазин.
        /// </summary>
        [HttpDelete("{shopId}")]
        public IActionResult Delete(int shopId)
        {
            var shop = Context.Shops
                .Where(x => x.ShopId == shopId)
                .FirstOrDefault();

            if (shop == null)
            {
                return BadRequest("Not Found");
            }

            Context.Shops.Remove(shop);
            Context.SaveChanges();

            return Ok();
        }
    }
}
