using System;
using Network.Contracts.Models.Interfaces;

namespace Network.Models
{
    public class ApiToken
    {
        #region Fields

        private IToken _token;

        #endregion

        #region Constructors

        public ApiToken()
        {
        }

        public ApiToken(IToken token)
        {
            _token = token;
        }

        #endregion

        public bool IsValidToken()
        {
            if (_token != null && !string.IsNullOrEmpty(_token.GetToken()))
            {
                var tokenMetadata = GetTokenMetadata();
                if (tokenMetadata != null)
                {
                    var datetimeExpired = tokenMetadata.GetTokenExpirationDate();
                    return datetimeExpired != null && datetimeExpired > DateTime.UtcNow;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void AddToken(IToken token)
        {
            _token = token;
        }

        public string GetToken()
        {
            return _token?.GetToken();
        }

        public void RemoveToken()
        {
            _token = null;
        }

        #region Private Methods

        private ITokenMetadata GetTokenMetadata()
        {
            if (_token != null && _token is ITokenMetadata)
            {
                return (ITokenMetadata)_token;
            }

            return null;
        }

        #endregion
    }
}
