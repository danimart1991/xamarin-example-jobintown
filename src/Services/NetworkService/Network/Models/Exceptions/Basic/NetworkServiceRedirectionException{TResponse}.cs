using System.Net;

namespace Network.Models.Exceptions.Basic
{
    public class NetworkServiceRedirectionException<TResponse> : NetworkServiceException
    {
        private new const string Message = "Error while performing an api call 3xx. Se inner exception for details";

        public NetworkServiceRedirectionException(HttpStatusCode statusCode)
            : base(Message)
        {
            StatusCode = statusCode;
        }

        public NetworkServiceRedirectionException(TResponse response, HttpStatusCode statusCode)
            : base(Message)
        {
            Response = response;
            StatusCode = statusCode;
        }

        public TResponse Response { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }
    }
}
