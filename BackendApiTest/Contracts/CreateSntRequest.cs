namespace BackendApiTest.Contracts.Snt
{
    public class CreateSntRequest
    {
        public string SntName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string ManagerFirstName { get; set; } = null!;
        public string ManagerLastName { get; set; } = null!;
        public string ManagerPhone { get; set; } = null!;
    }
}
