using System;

namespace Network.Contracts.Models.Exceptions
{
    public class NetworkServiceContentTypeException : Exception
    {
        private new const string Message = "Error in ContentType is not application/json and TResponse don´t implement IFile interface.";

        public NetworkServiceContentTypeException()
            : base(Message)
        {
        }
    }
}
