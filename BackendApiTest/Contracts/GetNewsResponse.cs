namespace BackendApiTest.Contracts.News
{
    public class GetNewsResponse
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string NewsText { get; set; } = null!;
        public DateTime NewsDate { get; set; }
        public int UserId { get; set; }
    }
}
