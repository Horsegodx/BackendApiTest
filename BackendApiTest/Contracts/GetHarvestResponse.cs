namespace BackendApiTest.Contracts.Harvest
{
    public class GetHarvestResponse
    {
        public int HarvestId { get; set; }
        public DateTime HarvestDate { get; set; }
        public int PlantId { get; set; }
    }
}
