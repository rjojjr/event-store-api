namespace event_store_api.Models
{
    public class GenericEventHttpModel
    {
        public string id { get; set; } = null!;

        public string eventStream { get; set; } = null!;

        public string eventName { get; set; } = null!;

        public IList<EventProperty> eventProperties { get; set; } = new List<EventProperty>();

 
        public static GenericEventHttpModel FromEntity(EventEntity eventEntity)
        {
            var model = new GenericEventHttpModel();
            model.id = eventEntity.Id;
            model.eventStream = eventEntity.eventStream;
            model.eventName = eventEntity.eventName;
            model.eventProperties = eventEntity.eventProperties;

            return model;
        }
    }
}
