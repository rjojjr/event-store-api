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
        public int submitEvent(GenericEventHttpRequestModel genericEvent)
        {
            _logger.LogInformation("received generic event submission request for stream {}", genericEvent.eventStream);
            _genericEventService.publishEvent(genericEvent);
            _logger.LogInformation("completed generic event submission request for stream {}", genericEvent.eventStream);
            return 200;   
        }

        [HttpGet]
        public List <GenericEventHttpModel> getEvents()
        {
            _logger.LogInformation("received get generic event submissions request");
            List<GenericEventHttpModel> publishedEvents = _genericEventService.getPublishedEvents();
            _logger.LogInformation("completed get generic event submissions request");
            return publishedEvents;
        }
    }
}
