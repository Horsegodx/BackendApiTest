namespace BackendApiTest.Contracts.Animal
{
    public class CreateAnimalsRequest
    {
        public string AnimalName { get; set; } = null!;
        public DateTime? AnimalBirthDate { get; set; }
        public string AnimalType { get; set; } = null!;
    }
}
