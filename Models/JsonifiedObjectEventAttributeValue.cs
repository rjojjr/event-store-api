using MongoDB.Bson;
using System.Text.Json;

namespace event_store_api.Models
{
    public class JsonifiedObjectEventAttributeValue : DefaultEventAttributeValue
    {

        public JsonifiedObjectEventAttributeValue(object rawEventPropertyValue) : base("json", Serialize(rawEventPropertyValue)) {}

        private static string Serialize(object rawObject)
        {
            return JsonSerializer.Serialize(rawObject);
        }

        public object ExtractRawValue()
        {
            return this.EventAttributeValue != null ? JsonSerializer.Deserialize<object>(EventAttributeValue) : null;
        }
    }
}
