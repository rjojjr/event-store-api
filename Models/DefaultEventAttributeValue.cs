using System.Text.Json.Serialization;

namespace event_store_api.Models
{
    public class DefaultEventAttributeValue : IEventAttributeValue
    {
        [JsonPropertyName("valueType")]
        public string ValueType { get; set; } = null!;
        [JsonPropertyName("eventAttributeValue")]
        public string EventAttributeValue { get; set; } = null!;

        public DefaultEventAttributeValue(string ValueType, string EventAttributeValue)
        {
            this.ValueType = ValueType;
            this.EventAttributeValue = EventAttributeValue;
        }
    }
}
