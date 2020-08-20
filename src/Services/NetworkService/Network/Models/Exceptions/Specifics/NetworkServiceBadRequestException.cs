using System.Net;
using Network.Contracts.Models.Interfaces;
using Network.Models.Exceptions.Basic;

namespace Network.Models.Exceptions.Specifics
{
    public class NetworkServiceBadRequestException : NetworkServiceClientErrorException
    {
        public NetworkServiceBadRequestException(IError error)
            : base(error, HttpStatusCode.BadRequest)
        {
        }
    }
}
