namespace BackendApiTest.Contracts.Poll
{
    public class CreatePollRequest
    {
        public string PollQuestion { get; set; } = null!;
        public DateTime PollDate { get; set; }
    }
}
