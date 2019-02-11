using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Tracing;

namespace UserViewer.Logging
{
    /// <summary>
    /// Simple trace writer to demonstrate logging.
    /// This provides details to the output window rather than persistant storage.
    /// </summary>
    public class TraceWriter : ITraceWriter
    {
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            TraceRecord record = new TraceRecord(request, category, level);
            traceAction(record);
            WriteTraceToOutputWindow(record);
        }

        public void WriteTraceToOutputWindow(TraceRecord record)
        {
            System.Diagnostics.Debug.WriteLine($"{record.Operation};{record.Operator}", record.Category);
        }
    }
}