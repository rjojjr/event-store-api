using System.Text.Json.Serialization;

namespace event_store_api.Models
{
    public class GenericEventHttpRequestModel
    {

        [JsonPropertyName("eventStream")]
        public string EventStream { get; set; } = null!;

        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = null!;

        [JsonPropertyName("eventName")]
        public string EventName { get; set; } = null!;

        [JsonPropertyName("eventAttributes")]
        public IList<EventAttribute> EventAttributes { get; set; } = new List<EventAttribute>(); 

    }
}
