namespace BackendApiTest.Contracts.Fertilization
{
    public class GetFertilizationResponse
    {
        public int FertilizationId { get; set; }
        public DateTime FertilizationDate { get; set; }
        public string FertilizerName { get; set; } = null!;
        public int PlantId { get; set; }
    }
}
