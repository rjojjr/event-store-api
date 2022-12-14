using System;
using System.Net;
using bagend_web_scraper.StockMarket.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace event_store_api.Controllers
{
	public class BaseController : ControllerBase
	{

        public IActionResult ExecuteWithExceptionHandler(Func<IActionResult> func)
        {
            return (IActionResult)ExecuteWithExceptionHandler(func);
        }

        public T ExecuteWithExceptionHandler<T>(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (HttpException e)
            {
                throw e.GetHttpRequestException();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message, e, HttpStatusCode.InternalServerError);
            }
        }

        public void ExecuteWithExceptionHandler(Action func)
        {
            try
            {
                func.Invoke();
            }
            catch (HttpException e)
            {
                throw e.GetHttpRequestException();
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message, e, HttpStatusCode.InternalServerError);
            }
        }
    }
}

