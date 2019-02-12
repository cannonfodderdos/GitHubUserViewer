using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserViewer.Logging
{
    /// <summary>
    /// Sample logger that just writes to output window
    /// </summary>
    public class OutputLogger : ILogger
    {
        private readonly LogLevel _logLevel;
        private readonly string _name;

        public OutputLogger(LogLevel logLevel, string name)
        {
            _logLevel = logLevel;
            _name = name;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logLevel <= logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // Check to see if we shoud log based on logLevel set when creating logger
            if (!IsEnabled(logLevel))
                return;

            System.Diagnostics.Debug.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");
        }
    }
}