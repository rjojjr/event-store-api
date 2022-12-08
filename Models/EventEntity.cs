namespace event_store_api.Models
{

    public class EventEntity
    {

        public EventEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; } = null!;

        public string EventStream { get; set; } = null!;

        public string EventName { get; set; } = null!;

        public IList<EventAttribute> EventAttributes { get; set; } = null!;

        public DateTime PersistedAt { get; set; }

        public EventEntity WithCurrentTime()
        {
            PersistedAt = DateTime.UtcNow;
            return this;
        }
    }
}
