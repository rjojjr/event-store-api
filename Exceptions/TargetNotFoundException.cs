using System;
using System.Net;

namespace bagend_web_scraper.StockMarket.Exceptions
{
	public class TargetNotFoundException : HttpException
    {
		public string TargetEntityId { get; set; } = null!;

		public TargetNotFoundException(string id) : base("target entity " + id + " not found", HttpStatusCode.NotFound)
		{
			TargetEntityId = id;
		}
	}
}

