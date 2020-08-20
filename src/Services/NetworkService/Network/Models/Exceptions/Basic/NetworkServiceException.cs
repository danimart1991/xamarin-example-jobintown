using System;

namespace Network.Models.Exceptions.Basic
{
    public class NetworkServiceException : Exception
    {
        private new const string Message = "Error while performing an api call. Se inner exception for details";

        public NetworkServiceException()
        {
        }

        public NetworkServiceException(Exception exception)
            : base(Message, exception)
        {
        }

        public NetworkServiceException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public NetworkServiceException(string message)
            : base(message)
        {
        }
    }
}
