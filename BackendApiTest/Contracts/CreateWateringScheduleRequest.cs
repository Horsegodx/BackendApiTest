namespace BackendApiTest.Contracts.WateringSchedule
{
    public class CreateWateringScheduleRequest
    {
        public DateTime WateringDate { get; set; }
        public TimeSpan WateringTime { get; set; }
    }
}
