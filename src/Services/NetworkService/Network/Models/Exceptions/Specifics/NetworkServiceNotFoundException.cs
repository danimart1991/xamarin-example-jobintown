using System.Net;
using Network.Models.Exceptions.Basic;

namespace Network.Models.Exceptions.Specifics
{
    public class NetworkServiceNotFoundException : NetworkServiceClientErrorException
    {
        public NetworkServiceNotFoundException(string url)
            : base(HttpStatusCode.NotFound)
        {
        }

        public string Url { get; set; }
    }
}
