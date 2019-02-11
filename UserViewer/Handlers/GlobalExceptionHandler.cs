using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace UserViewer.Handlers
{
    /// <summary>
    /// GlobalExceptionHandler to provide prettier messages back to client for any unhandled exceptions
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {

            // Begin building HttpResponseMessage to send back to client
            var result = new HttpResponseMessage();

            result.StatusCode = HttpStatusCode.InternalServerError;
            result.Content = new StringContent("There was an error processing your request. Please contact support@joebloggs.com.");

            // If it's an ApiException, we can determine specifics such as status code
            // and hide any detail client shouldn't see. This detail should still be logged.
            if (context.Exception is ApiException)
            {
                ApiException exception = context.Exception as ApiException;

                if (exception.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.Content = new StringContent("The specified resource couldn't be found.");
                }
            }

            // Any additional errors we may want to handle in a different way before serving client

            // Set result
            context.Result = new ExceptionHttpResult(result);
        }
    }

    public class ExceptionHttpResult : IHttpActionResult
    {
        private HttpResponseMessage _httpResponseMessage;

        public ExceptionHttpResult(HttpResponseMessage httpResponseMessage)
        {
            _httpResponseMessage = httpResponseMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_httpResponseMessage);
        }
    }
}