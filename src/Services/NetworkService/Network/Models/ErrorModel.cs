using Network.Contracts.Models.Interfaces;

namespace Network.Models
{
    public class ErrorModel : IError
    {
        public string Error { get; set; }

        public string GetErrorMessage()
        {
            return Error;
        }
    }
}
