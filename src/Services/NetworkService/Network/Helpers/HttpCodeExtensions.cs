using System.Net;
using Network.Models;

namespace Network.Helpers
{
    public static class HttpCodeExtensions
    {
        public static HttpCodeGroupEnum GetHttpCodeTypeEnum(this HttpStatusCode httpCode)
        {
            var result = HttpCodeGroupEnum.None;
            var code = (int)httpCode;
            if (code >= 100 && code < 200)
            {
                result = HttpCodeGroupEnum.Informational;
            }
            else if (code >= 200 && code < 300)
            {
                result = HttpCodeGroupEnum.Successful;
            }
            else if (code >= 300 && code < 400)
            {
                result = HttpCodeGroupEnum.Redirection;
            }
            else if (code >= 400 && code < 500)
            {
                result = HttpCodeGroupEnum.ClientError;
            }
            else if (code >= 500)
            {
                result = HttpCodeGroupEnum.ServerError;
            }

            return result;
        }
    }
}
