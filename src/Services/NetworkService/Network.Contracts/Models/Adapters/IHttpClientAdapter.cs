using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Network.Contracts.Models.Adapters
{
    public interface IHttpClientAdapter
    {
        AuthenticationHeaderValue Authorization { get; set; }

        Task<HttpResponseMessage> GetAsync(string requestUri);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value);

        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}
