namespace event_store_api.Models
{
    public class EventAttribute
    {
        public string eventAttributeName { get; set; } = null!;
        public DefaultEventAttributeValue eventAttributeValue { get; set; } = null!;

    }
}
