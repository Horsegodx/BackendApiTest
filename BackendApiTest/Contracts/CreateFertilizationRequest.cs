namespace BackendApiTest.Contracts.Fertilization
{
    public class CreateFertilizationRequest
    {
        public DateTime FertilizationDate { get; set; }
        public string FertilizerName { get; set; } = null!;
        public int PlantId { get; set; }
    }
}
