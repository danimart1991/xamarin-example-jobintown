using System.Net.Http;
using System.Threading.Tasks;
using Network.Contracts.Models.Interfaces;
using Newtonsoft.Json;

namespace Network.Models
{
    public class ErrorConverter : IErrorModelConverter
    {
        public async Task<IError> ConvertError<TError>(HttpResponseMessage response)
            where TError : IError
        {
            IError result = null;
            if (response.Content != null)
            {
                try
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TError>(contentString);
                }
                catch
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    result = new ErrorModel() { Error = msg };
                }
            }

            return result;
        }
    }
}
