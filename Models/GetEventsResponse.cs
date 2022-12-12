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
        public int ResultCount { get; set; }

        [JsonPropertyName("results")]
        public IList<GenericEventHttpModel> Results { get; set; } = new List<GenericEventHttpModel>();
	}
}

