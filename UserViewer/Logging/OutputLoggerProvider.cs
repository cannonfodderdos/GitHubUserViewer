using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserViewer.Logging
{
    public class OutputLoggerProvider : ILoggerProvider
    {
        private LogLevel _logLevel;
        private readonly ConcurrentDictionary<string, OutputLogger> _loggers = new ConcurrentDictionary<string, OutputLogger>();

        public OutputLoggerProvider(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new OutputLogger(_logLevel, name));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}