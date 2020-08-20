using System.Net;
using Network.Contracts.Models.Interfaces;
using Network.Models.Exceptions.Basic;

namespace Network.Models.Exceptions.Specifics
{
    public class NetworkServiceUnauthorizedException : NetworkServiceClientErrorException
    {
        public NetworkServiceUnauthorizedException()
            : base(HttpStatusCode.Unauthorized)
        {
        }

        public NetworkServiceUnauthorizedException(IError error)
            : base(error, HttpStatusCode.Unauthorized)
        {
        }
    }
}
