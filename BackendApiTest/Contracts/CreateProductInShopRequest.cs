namespace BackendApiTest.Contracts.ProductInShop
{
    public class CreateProductInShopRequest
    {
        public string ProductName { get; set; } = null!;
        public int ProductPrice { get; set; }
        public int ShopId { get; set; }
    }
}
