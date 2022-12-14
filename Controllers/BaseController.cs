using System;
using System.Net;
using bagend_web_scraper.StockMarket.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace event_store_api.Controllers
{
	public class BaseController : ControllerBase
	{

        internal IActionResult ExecuteWithExceptionHandler(Func<IActionResult> func)
        {
            return (IActionResult)ExecuteWithExceptionHandler(func);
        }

        internal T ExecuteWithExceptionHandler<T>(Func<T> func)
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

        internal void ExecuteWithExceptionHandler(Action func)
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

