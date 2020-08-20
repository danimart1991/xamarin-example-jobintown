using Network.Contracts.Models.Interfaces;
using Newtonsoft.Json;

namespace JobInTown.Azure.Client.Models
{
    public class LoginError : IError
    {
        [JsonProperty("error")]
        public string Title { get; set; }

        [JsonProperty("error_description")]
        public string Description { get; set; }

        public string GetErrorMessage()
        {
            return Description;
        }
    }
}
