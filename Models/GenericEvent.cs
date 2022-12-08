namespace event_store_api.Models
{
    public class GenericEvent
    {
        public string eventStream { get; set; } = null!;

        public string eventName { get; set; } = null!;

        public IDictionary<string, object> eventProperties = IDictionary<string, object>
    }
}
