namespace BackendApiTest.Contracts.SntEvent
{
    public class CreateSntEventRequest
    {
        public DateTime EventDate { get; set; }
        public string EventName { get; set; } = null!;
        public string EventLocation { get; set; } = null!;
    }
}
