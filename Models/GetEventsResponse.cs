using System;
namespace event_store_api.Models
{
	public class GetEventsResponse
	{
        public GetEventsResponse(int resultCount, IList<GenericEventHttpModel> results)
        {
            ResultCount = resultCount;
            Results = results;
        }

        private int ResultCount { get; set; }

		private IList<GenericEventHttpModel> Results { get; set; } = new List<GenericEventHttpModel>();
	}
}

