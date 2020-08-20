using System.Collections.Generic;
using System.Text;
using Network.Contracts.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JobInTown.Azure.Client.Models
{
    public class RegisterError : IError
    {
        public string Message { get; set; }

        [JsonProperty("ModelState")]
        public Dictionary<string, object> ModelStates { get; set; }

        public string GetErrorMessage()
        {
            StringBuilder message = new StringBuilder();

            if (ModelStates != null)
            {
                foreach (var modelState in ModelStates)
                {
                    if (modelState.Value is JArray errors)
                    {
                        foreach (var error in errors)
                        {
                            message.AppendLine(error.ToString());
                        }
                    }
                }
            }

            return message.ToString();
        }
    }
}
