namespace event_store_api.Models
{
    public class GenericEventHttpRequestModel
    {

        public string eventStream { get; set; } = null!;

        public string eventName { get; set; } = null!;

        public IList<EventAttribute> eventAttributes { get; set; } = new List<EventAttribute>();

    }
}
