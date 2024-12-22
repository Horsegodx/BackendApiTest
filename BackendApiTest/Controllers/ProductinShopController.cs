using BackendApiTest.Contracts.ProductInShop;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductinShopController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public ProductinShopController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить все товары в магазинах.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var productsInShop = Context.ProductInShops
                .ProjectToType<GetProductInShopResponse>()
                .ToList();
            return Ok(productsInShop);
        }

        /// <summary>
        /// Получить товар по его идентификатору товара и идентификатору магазина.
        /// </summary>
        [HttpGet("{productId}/{shopId}")]
        public IActionResult GetById(int productId, int shopId)
        {
            var productInShop = Context.ProductInShops
                .Where(x => x.ProductId == productId && x.ShopId == shopId)
                .ProjectToType<GetProductInShopResponse>()
                .FirstOrDefault();

            if (productInShop == null)
            {
                return BadRequest("Not Found");
            }

            return Ok(productInShop);
        }

        /// <summary>
        /// Добавить новый товар в магазин.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreateProductInShopRequest request)
        {
            var productInShop = request.Adapt<ProductInShop>();
            Context.ProductInShops.Add(productInShop);
            Context.SaveChanges();

            var response = productInShop.Adapt<GetProductInShopResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Обновить информацию о товаре в магазине.
        /// </summary>
        [HttpPut]
        public IActionResult Update(CreateProductInShopRequest request, int productId, int shopId)
        {
            var existingProductInShop = Context.ProductInShops
                .Where(x => x.ProductId == productId && x.ShopId == shopId)
                .FirstOrDefault();

            if (existingProductInShop == null)
            {
                return BadRequest("Not Found");
            }

            request.Adapt(existingProductInShop);
            Context.SaveChanges();

            var response = existingProductInShop.Adapt<GetProductInShopResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Удалить товар из магазина.
        /// </summary>
        [HttpDelete("{productId}/{shopId}")]
        public IActionResult Delete(int productId, int shopId)
        {
            var productInShop = Context.ProductInShops
                .Where(x => x.ProductId == productId && x.ShopId == shopId)
                .FirstOrDefault();

            if (productInShop == null)
            {
                return BadRequest("Not Found");
            }

            Context.ProductInShops.Remove(productInShop);
            Context.SaveChanges();

            return Ok();
        }
    }
}
