namespace event_store_api.Models
{
    public class DefaultEventAttributeValue : IEventAttributeValue
    {
        public string ValueType { get; set; } = null!;
        public string EventAttributeValue { get; set; } = null!;

        public DefaultEventAttributeValue(string ValueType, string EventAttributeValue)
        {
            this.ValueType = ValueType;
            this.EventAttributeValue = EventAttributeValue;
        }
    }
}
