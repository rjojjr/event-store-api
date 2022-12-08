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
        public void SubmitEvent(GenericEventHttpRequestModel genericEvent)
        {
            _logger.LogInformation("received generic event submission request for stream {}", genericEvent.EventStream);
            _genericEventService.PublishEvent(genericEvent);
            _logger.LogInformation("completed generic event submission request for stream {}", genericEvent.EventStream); 
        }

        [HttpGet]
        public IList <GenericEventHttpModel> GetEvents()
        {
            _logger.LogInformation("received get generic event submissions request");
            IList<GenericEventHttpModel> publishedEvents = _genericEventService.GetPublishedEvents();
            _logger.LogInformation("completed get generic event submissions request");
            return publishedEvents;
        }
    }
}
