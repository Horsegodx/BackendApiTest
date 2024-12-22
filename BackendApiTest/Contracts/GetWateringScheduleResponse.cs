namespace BackendApiTest.Contracts.WateringSchedule
{
    public class GetWateringScheduleResponse
    {
        public int WateringScheduleId { get; set; }
        public DateTime WateringDate { get; set; }
        public TimeSpan WateringTime { get; set; }
    }
}
