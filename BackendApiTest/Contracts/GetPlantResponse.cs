namespace BackendApiTest.Contracts.Plant
{
    public class GetPlantResponse
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; } = null!;
        public string PlantType { get; set; } = null!;
    }
}
