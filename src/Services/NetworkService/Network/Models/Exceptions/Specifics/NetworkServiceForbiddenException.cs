using System.Net;
using Network.Contracts.Models.Interfaces;
using Network.Models.Exceptions.Basic;

namespace Network.Models.Exceptions.Specifics
{
    public class NetworkServiceForbiddenException : NetworkServiceClientErrorException
    {
        public NetworkServiceForbiddenException()
            : base(HttpStatusCode.Forbidden)
        {
        }

        public NetworkServiceForbiddenException(IError error)
            : base(error, HttpStatusCode.Forbidden)
        {
        }
    }
}
