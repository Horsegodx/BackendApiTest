namespace BackendApiTest.Contracts.SntEvent
{
    public class GetSntEventResponse
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventName { get; set; } = null!;
        public string EventLocation { get; set; } = null!;
    }
}
