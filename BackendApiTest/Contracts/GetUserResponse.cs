namespace BackendApiTest.Contracts.User
{
    public class GetUserResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

}
