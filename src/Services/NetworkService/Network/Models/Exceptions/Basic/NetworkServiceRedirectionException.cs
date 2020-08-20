using System.Net;

namespace Network.Models.Exceptions.Basic
{
    public class NetworkServiceRedirectionException : NetworkServiceException
    {
        private new const string Message = "Error while performing an api call 3xx. Se inner exception for details";

        public NetworkServiceRedirectionException(HttpStatusCode statusCode)
            : base(Message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }
    }
}
