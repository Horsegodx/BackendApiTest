namespace BackendApiTest.Contracts.Shop
{
    public class GetShopResponse
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
    }
}
