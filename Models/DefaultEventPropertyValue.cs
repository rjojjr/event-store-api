namespace event_store_api.Models
{
    public class DefaultEventPropertyValue : IEventPropertyValue
    {
        public string ValueType { get; set; } = null!;
        public string EventPropertyValue { get; set; } = null!;

        public DefaultEventPropertyValue(string ValueType, string EventPropertyValue)
        {
            this.ValueType = ValueType;
            this.EventPropertyValue = EventPropertyValue;
        }
    }
}
