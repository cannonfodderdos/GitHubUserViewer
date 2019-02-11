using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    /// <summary>
    /// Custom exception for handling unexpected behaviours from API class libraries
    /// </summary>
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException (int statusCode, string message, Exception inner) : base (message, inner)
        {
            StatusCode = statusCode;
        }
    }
}
