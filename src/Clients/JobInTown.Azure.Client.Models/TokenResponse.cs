using System;
using Network.Contracts.Models.Interfaces;
using Newtonsoft.Json;

namespace JobInTown.Azure.Client.Models
{
    public class TokenResponse : IToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }

        public string GetToken()
        {
            return AccessToken;
        }
    }
}
