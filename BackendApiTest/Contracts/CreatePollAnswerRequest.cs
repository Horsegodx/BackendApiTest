namespace BackendApiTest.Contracts.PollAnswer
{
    public class CreatePollAnswerRequest
    {
        public int UserId { get; set; }
        public int OptionId { get; set; }
        public int PollId { get; set; }
    }
}
