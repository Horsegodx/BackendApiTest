namespace BackendApiTest.Contracts.Message
{
    public class CreateMessageRequest
    {
        public string MessageText { get; set; } = null!;
        public TimeSpan MessageTime { get; set; }
        public DateTime MessageDate { get; set; }
        public int UserId { get; set; }
    }
}
