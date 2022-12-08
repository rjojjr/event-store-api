namespace event_store_api.Models
{
    public class EventProperty
    {
        public string eventProperty { get; set; } = null!;
        public DefaultEventPropertyValue eventPropertyValue { get; set; } = null!;

    }
}
