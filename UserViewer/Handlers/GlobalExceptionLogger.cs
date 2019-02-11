using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace UserViewer.Handlers
{
    /// <summary>
    /// GlobalExceptionLogger to record all unhandled exceptions
    /// </summary>
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var message = context.Exception.Message;
            var stacktrace = context.Exception.StackTrace;

            // Log here
            System.Diagnostics.Debug.WriteLine($"{message};{stacktrace}");

        }
    }
}