namespace event_store_api.Models
{
    public class GenericEventHttpModel : GenericEventHttpRequestModel
    {
        public string id { get; set; } = null!;
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
