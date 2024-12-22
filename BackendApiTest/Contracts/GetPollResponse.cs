namespace BackendApiTest.Contracts.Poll
{
    public class GetPollResponse
    {
        public int PollId { get; set; }
        public string PollQuestion { get; set; } = null!;
        public DateTime PollDate { get; set; }
    }
}
