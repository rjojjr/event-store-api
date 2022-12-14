using System;
using System.Net;

namespace bagend_web_scraper.StockMarket.Exceptions
{
	public class HttpException : SystemException
    {

		private string _message { get; set; } = null!;
        private HttpStatusCode _statusCode { get; set; }

        public HttpException(string message, HttpStatusCode statusCode) : base(message)
		{
			_message = message;
			_statusCode = statusCode;
		}

		public HttpRequestException GetHttpRequestException()
		{ 
			return new HttpRequestException(_message, this, _statusCode);
		}
	}
}

