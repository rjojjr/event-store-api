using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace event_store_api.Models
{

    public class EventEntity
    {

        public EventEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("stream")]
        public string eventStream { get; set; } = null!;

        public string eventName { get; set; } = null!;

        public string eventProperty { get; set; } = null!;

        public string eventValue { get; set; } = null!;
    }
}
