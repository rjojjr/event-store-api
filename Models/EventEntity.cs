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

        public string Id { get; set; } = null!;

        public string eventStream { get; set; } = null!;

        public string eventName { get; set; } = null!;

        public IList<EventAttribute> eventAttributes { get; set; } = null!;

        public DateTime persistedAt { get; set; }

        public EventEntity withCurrentTime()
        {
            persistedAt = DateTime.UtcNow;
            return this;
        }
    }
}
