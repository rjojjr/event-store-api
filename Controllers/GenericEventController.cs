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
        public IActionResult SubmitEvent([FromBody] GenericEventHttpRequestModel genericEvent)
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
        public IActionResult GetEvents([FromQuery]string eventStream="", [FromQuery]string eventName="")
        {
            var parsedStream = eventStream.Equals("") ? null : eventStream;
            var parsedName = eventName.Equals("") ? null : eventName;

            return ExecuteWithExceptionHandler(() =>
            {
                _logger.LogInformation("received get generic event submissions request stream = {}, name = {}", parsedStream, parsedName);
                IList<GenericEventHttpModel> publishedEvents = _genericEventService.GetPublishedEvents(parsedStream, parsedName);
                _logger.LogInformation("completed get generic event submissions request stream = {}, name = {}", parsedStream, parsedName);
                return Ok(new GetEventsResponse(publishedEvents.Count(), publishedEvents));
            });
        }

        [HttpGet("attribute")]
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
