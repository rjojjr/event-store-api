using MongoDB.Bson;
using System.Text.Json;

namespace event_store_api.Models
{
    public class JsonfiedObjectEventPropertyValue : DefaultEventPropertyValue
    {

        public JsonfiedObjectEventPropertyValue(object rawEventPropertyValue) : base("json", Serialize(rawEventPropertyValue)) {}

        private static string Serialize(object rawObject)
        {
            return JsonSerializer.Serialize(rawObject);
        }

        public object ExtractRawValue()
        {
            return this.EventPropertyValue != null ? JsonSerializer.Deserialize<object>(EventPropertyValue) : null;
        }
    }
}
