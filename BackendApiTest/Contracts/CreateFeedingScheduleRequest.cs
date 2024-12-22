namespace BackendApiTest.Contracts.FeedingSchedule
{
    public class CreateFeedingScheduleRequest
    {
        public TimeSpan FeedingTime { get; set; }
        public string FeedingType { get; set; } = null!;
        public DateTime FeedingDate { get; set; }
        public int AnimalsId { get; set; }
    }
}
