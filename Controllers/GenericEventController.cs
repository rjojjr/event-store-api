using event_store_api.Models;
using event_store_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace event_store_api.Controllers
{

    [ApiController]
    [Route("generic/events/api/v1")]
    public class GenericEventController : BaseController
    {
        private readonly ILogger<GenericEventController> _logger;
        private GenericEventService _genericEventService;

        public GenericEventController(ILogger<GenericEventController> logger, GenericEventService genericEventService)
        {
            _logger = logger;
            _genericEventService = genericEventService;
        }

        [HttpPost]
        public IActionResult SubmitEvent(GenericEventHttpRequestModel genericEvent)
        {
            return ExecuteWithExceptionHandler(() =>
            {
                _logger.LogInformation("received generic event submission request for stream {}", genericEvent.EventStream);
                _genericEventService.PublishEvent(genericEvent);
                _logger.LogInformation("completed generic event submission request for stream {}", genericEvent.EventStream);
                return Created(".", "success");
            });
        }

        [HttpGet]
        public IActionResult GetEvents(string eventStream, string eventName)
        {
            return ExecuteWithExceptionHandler(() =>
            {
                _logger.LogInformation("received get generic event submissions request stream = {}, name = {}", eventStream, eventName);
                IList<GenericEventHttpModel> publishedEvents = _genericEventService.GetPublishedEvents(eventStream, eventName);
                _logger.LogInformation("completed get generic event submissions request stream = {}, name = {}", eventStream, eventName);
                return Ok(new GetEventsResponse(publishedEvents.Count(), publishedEvents));
            });
        }

        [HttpGet]
        [Route("attribute")]
        public IActionResult GetEventsByAttribute([FromQuery] EventAttributeSearchParameters parameters)
        {
            return ExecuteWithExceptionHandler(() =>
            {
                _logger.LogInformation("received get generic event submissions by attribute request");
                IList<GenericEventHttpModel> publishedEvents = _genericEventService.getEventsWithAttribute(parameters.AttributeName, parameters.AttributeValue);
                _logger.LogInformation("completed get generic event submissions by attribute request");
                return Ok(new GetEventsResponse(publishedEvents.Count(), publishedEvents));
            });
        }
    }
}
