using event_store_api.Models;
using event_store_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace event_store_api.Controllers
{

    [ApiController]
    [Route("generic/events/api/v1")]
    public class GenericEventController
    {
        private readonly ILogger<GenericEventController> _logger;
        private GenericEventService _genericEventService;

        public GenericEventController(ILogger<GenericEventController> logger, GenericEventService genericEventService)
        {
            _logger = logger;
            _genericEventService = genericEventService;
        }

        [HttpPost]
       // [Route("")]
        public int submitEvent(GenericEvent genericEvent)
        {
            _logger.LogInformation("received generic event submission request for stream {}", genericEvent.eventStream);
            _genericEventService.publishEvent(genericEvent);
            return 200;   
        }

        [HttpGet]
        public List <EventEntity> getEvents()
        {
            _logger.LogInformation("received get generic event submissions request");
            return _genericEventService.getPublishedEvents();
        }
    }
}
