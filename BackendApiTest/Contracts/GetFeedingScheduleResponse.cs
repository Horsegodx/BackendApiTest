namespace BackendApiTest.Contracts.FeedingSchedule
{
    public class GetFeedingScheduleResponse
    {
        public TimeSpan FeedingTime { get; set; }
        public string FeedingType { get; set; } = null!;
        public int FeedingId { get; set; }
        public DateTime FeedingDate { get; set; }
        public int AnimalsId { get; set; }
    }
}
