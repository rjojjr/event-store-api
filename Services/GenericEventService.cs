using event_store_api.Models;
using event_store_api.Repository;
using SharpCompress.Common;

namespace event_store_api.Services
{
    public class GenericEventService
    {
        private GenericEventManager _genericEventManager;
        private GenericEventEntityRepository _eventEntityRepository;
        private readonly ILogger<GenericEventService> _logger;

        public GenericEventService(GenericEventEntityRepository genericEventEntityRepository, ILogger<GenericEventService> logger)
        {
            this._genericEventManager = new GenericEventManager(this.publishEvent);
            this._eventEntityRepository = genericEventEntityRepository;
            this._logger = logger;
        }

        public void publishEvent(GenericEventHttpRequestModel genericEvent)
        {
            this._genericEventManager.OnGenericEvent(genericEvent);
        }

        public List<GenericEventHttpModel> getPublishedEvents()
        {
            _logger.LogInformation("fetching published events");
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            List<EventEntity> events = _eventEntityRepository.GetAsync().Result.ToList();
            long timeTaken = DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds;
            this._logger.LogInformation("done fetching published events, found {} events, took {} millis", events.Count, timeTaken);
            List<GenericEventHttpModel> models = new List<GenericEventHttpModel>();
            foreach(EventEntity Event in events)
            {
                models.Add(GenericEventHttpModel.FromEntity(Event));
            }

            return models;

        }
        private void publishEvent(Object sender, GenericEventArgs e)
        {
            var entity = MapGenericEventEntity(e);
            this._logger.LogInformation("persisting generic event {}", entity.Id);
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            this._eventEntityRepository.CreateAsync(entity).Wait(new TimeSpan(0, 1, 0));
            long timeTaken = DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds;
            this._logger.LogInformation("done persisting generic event {}, took {} millis", entity.Id, timeTaken);
        }

        private static EventEntity MapGenericEventEntity(GenericEventArgs genericEvent)
        {
            var entity = new EventEntity();
            entity.eventStream = genericEvent.eventStream;
            entity.eventName = genericEvent.eventName;
            entity.eventAttributes = genericEvent.eventAttributes;

            return entity;
        }
    }
}
