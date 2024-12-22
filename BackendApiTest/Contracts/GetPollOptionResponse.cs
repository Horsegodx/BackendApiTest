namespace BackendApiTest.Contracts.PollOption
{
    public class GetPollOptionResponse
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; } = null!;
        public int PollId { get; set; }
    }
}
