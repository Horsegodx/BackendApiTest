namespace BackendApiTest.Contracts.PollAnswer
{
    public class GetPollAnswerResponse
    {
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public int OptionId { get; set; }
        public int PollId { get; set; }
    }
}
