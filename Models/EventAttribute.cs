using System.Text.Json.Serialization;

namespace event_store_api.Models
{
    public class EventAttribute
    {
        [JsonPropertyName("eventAttributeName")]
        public string EventAttributeName { get; set; } = null!;
        [JsonPropertyName("eventAttributeValue")]
        public DefaultEventAttributeValue EventAttributeValue { get; set; } = null!;

    }
}
