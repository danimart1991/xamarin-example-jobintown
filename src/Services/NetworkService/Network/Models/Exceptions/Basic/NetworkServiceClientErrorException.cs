using System.Net;
using Network.Contracts.Models.Interfaces;

namespace Network.Models.Exceptions.Basic
{
    public class NetworkServiceClientErrorException : NetworkServiceException
    {
        private new const string Message = "Error while performing an api call 4xx. Se inner exception for details";

        public NetworkServiceClientErrorException(HttpStatusCode statusCode)
            : base(Message)
        {
            StatusCode = statusCode;
        }

        public NetworkServiceClientErrorException(IError error, HttpStatusCode statusCode)
            : base(error != null ? error.GetErrorMessage() : Message)
        {
            Error = error;
            StatusCode = statusCode;
        }

        public IError Error { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }
    }
}
