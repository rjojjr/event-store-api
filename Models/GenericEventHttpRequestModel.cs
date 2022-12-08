namespace event_store_api.Models
{
    public class GenericEventHttpRequestModel
    {

        public string EventStream { get; set; } = null!;

        public string EventName { get; set; } = null!;

        public IList<EventAttribute> EventAttributes { get; set; } = new List<EventAttribute>();

    }
}
