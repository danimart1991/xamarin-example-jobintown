using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Network.Contracts.Models.Adapters;
using Newtonsoft.Json;

namespace Network.Adapters
{
    public class HttpClientAdapter : IHttpClientAdapter
    {
        #region Fields

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructor

        public HttpClientAdapter()
        {
            _httpClient = new HttpClient();
        }

        #endregion

        #region Properties

        public AuthenticationHeaderValue Authorization
        {
            get { return _httpClient.DefaultRequestHeaders?.Authorization; }
            set { _httpClient.DefaultRequestHeaders.Authorization = value; }
        }

        #endregion

        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return _httpClient.DeleteAsync(requestUri);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return _httpClient.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return _httpClient.PutAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _httpClient.SendAsync(request);
        }
    }
}
