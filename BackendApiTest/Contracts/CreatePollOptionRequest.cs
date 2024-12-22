namespace BackendApiTest.Contracts.PollOption
{
    public class CreatePollOptionRequest
    {
        public string OptionText { get; set; } = null!;
        public int PollId { get; set; }
    }
}
