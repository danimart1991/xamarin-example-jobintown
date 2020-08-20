using System.Net;
using Network.Contracts.Models.Interfaces;
using Network.Models.Exceptions.Basic;

namespace Network.Models.Exceptions.Specifics
{
    public class NetworkServiceInternalServerErrorException : NetworkServiceServerErrorException
    {
        public NetworkServiceInternalServerErrorException(IError error)
            : base(error, HttpStatusCode.InternalServerError)
        {
        }
    }
}
