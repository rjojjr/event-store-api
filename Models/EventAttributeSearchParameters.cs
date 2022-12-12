using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace event_store_api.Models
{
	public class EventAttributeSearchParameters
	{
		public EventAttributeSearchParameters()
		{
		}

        [BindRequired]
        public string AttributeName { get; set; } = null!;

        [BindRequired]
        public string AttributeValue { get; set; } = null!;
    }
}

