namespace BackendApiTest.Contracts.Animal
{
    public class GetAnimalsResponse
    {
        public int AnimalsId { get; set; }
        public string AnimalName { get; set; } = null!;
        public DateTime? AnimalBirthDate { get; set; }
        public string AnimalType { get; set; } = null!;
    }
}
