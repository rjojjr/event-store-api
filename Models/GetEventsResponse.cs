using System;
using System.Text.Json.Serialization;

namespace event_store_api.Models
{
	public class GetEventsResponse
	{

        public GetEventsResponse()
        {

        }

        public GetEventsResponse(int resultCount, IList<GenericEventHttpModel> results)
        {
            ResultCount = resultCount;
            Results = results;
        }

        [JsonPropertyName("resultCount")]
        private int ResultCount { get; set; }

        [JsonPropertyName("results")]
        private IList<GenericEventHttpModel> Results { get; set; } = new List<GenericEventHttpModel>();
	}
}

