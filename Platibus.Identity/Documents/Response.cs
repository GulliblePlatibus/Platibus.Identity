using System;

namespace Platibus.Identity.Documents
{
    public class Response
    {
        public bool IsSuccessful { get; private set; }
        public string Message { get; private set; }
        public Exception Exception { get; private set; }

        public Response(bool isSuccessful, string message = null, Exception exception = null)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Exception = exception;
        }

        public static Response Successful()
        {
            return new Response(true);
        }

        public static Response Unsuccessful(string message = null, Exception exception = null)
        {
            return new Response(false, message, exception);
        }
    }
}