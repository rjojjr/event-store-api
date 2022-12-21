using event_store_api.Models;
using event_store_api.Repository;
using SharpCompress.Common;

namespace event_store_api.Services
{
    public class GenericEventService
    {
        private readonly GenericEventManager _genericEventManager;
        private readonly GenericEventEntityRepository _eventEntityRepository;
        private readonly ILogger<GenericEventService> _logger;

        public GenericEventService(GenericEventEntityRepository genericEventEntityRepository, ILogger<GenericEventService> logger)
        {
            this._genericEventManager = new GenericEventManager(this.PublishEvent);
            this._eventEntityRepository = genericEventEntityRepository;
            this._logger = logger;
        }

        public void PublishEvent(GenericEventHttpRequestModel genericEvent)
        {
            this._genericEventManager.OnGenericEvent(genericEvent);
        }

        public IList<GenericEventHttpModel> getEventsWithAttribute(string attributeName, string attributeValue)
        {
            var entities = _eventEntityRepository.FindByEventAttributeValue(attributeName, attributeValue);

            return genericEventHttpModels(entities);
        }

        public IList<GenericEventHttpModel> getEventsWithAttributes(IList<string> filters)
        {
            var entities = _eventEntityRepository.FindByEventAttributeValues(filters);

            return genericEventHttpModels(entities);
        }

        public IList<GenericEventHttpModel> GetPublishedEvents(string eventStream, string eventName)
        {
            _logger.LogInformation("fetching published events");
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            List<EventEntity> events = _eventEntityRepository.GetAsync(eventStream, eventName).Result.ToList();
            long timeTaken = DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds;
            this._logger.LogInformation("done fetching published events, found {} events, took {} millis", events.Count, timeTaken);

            return genericEventHttpModels(events);
        }

        private IList<GenericEventHttpModel> genericEventHttpModels(IList<EventEntity> events)
        {
            List<GenericEventHttpModel> models = new List<GenericEventHttpModel>();
            foreach (EventEntity Event in events)
            {
                models.Add(GenericEventHttpModel.FromEntity(Event));
            }

            return models;
        }
        private void PublishEvent(Object sender, GenericEventArgs e)
        {
            var entity = MapGenericEventEntity(e);
            this._logger.LogInformation("persisting generic event {}", entity.Id);
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            this._eventEntityRepository.CreateAsync(entity).Wait(new TimeSpan(0, 3, 0));
            long timeTaken = DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds;
            this._logger.LogInformation("done persisting generic event {}, took {} millis", entity.Id, timeTaken);
        }

        private static EventEntity MapGenericEventEntity(GenericEventArgs genericEvent)
        {
            var entity = new EventEntity();
            entity.EventStream = genericEvent.EventStream;
            entity.EventType = genericEvent.EventType;
            entity.EventName = genericEvent.EventName;
            entity.EventAttributes = genericEvent.EventAttributes;

            return entity;
        }
    }
}
