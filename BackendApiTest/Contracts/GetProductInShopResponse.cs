namespace BackendApiTest.Contracts.ProductInShop
{
    public class GetProductInShopResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductPrice { get; set; }
        public int ShopId { get; set; }
    }
}
