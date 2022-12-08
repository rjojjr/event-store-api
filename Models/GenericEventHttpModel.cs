namespace event_store_api.Models
{
    public class GenericEventHttpModel : GenericEventHttpRequestModel
    {
        public string Id { get; set; } = null!;

        public DateTime PersistedAt { get; set; }

        public static GenericEventHttpModel FromEntity(EventEntity eventEntity)
        {
            var model = new GenericEventHttpModel();
            model.Id = eventEntity.Id;
            model.EventStream = eventEntity.EventStream;
            model.EventName = eventEntity.EventName;
            model.EventAttributes = eventEntity.EventAttributes;
            model.PersistedAt = eventEntity.PersistedAt;
            model.EventType = eventEntity.EventType;

            return model;
        }
    }
}
