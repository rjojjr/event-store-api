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
        public string SubmitEvent(GenericEventHttpRequestModel genericEvent)
        {
            _logger.LogInformation("received generic event submission request for stream {}", genericEvent.EventStream);
            _genericEventService.PublishEvent(genericEvent);
            _logger.LogInformation("completed generic event submission request for stream {}", genericEvent.EventStream);
            return "success";
        }

        [HttpGet]
        public GetEventsResponse GetEvents()
        {
            _logger.LogInformation("received get generic event submissions request");
            IList<GenericEventHttpModel> publishedEvents = _genericEventService.GetPublishedEvents();
            _logger.LogInformation("completed get generic event submissions request");
            return new GetEventsResponse(publishedEvents.Count(), publishedEvents);
        }

        [HttpGet]
        [Route("attribute")]
        public GetEventsResponse GetEventsByAttribute([FromQuery] EventAttributeSearchParameters parameters)
        {
            _logger.LogInformation("received get generic event submissions by attribute request");
            IList<GenericEventHttpModel> publishedEvents = _genericEventService.getEventsWithAttribute(parameters.AttributeName, parameters.AttributeValue);
            _logger.LogInformation("completed get generic event submissions by attribute request");
            return new GetEventsResponse(publishedEvents.Count(), publishedEvents);
        }
    }
}
