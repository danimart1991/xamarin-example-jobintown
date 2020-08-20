using System;

namespace Network.Contracts.Models.Interfaces
{
    public interface ITokenMetadata : IToken
    {
        DateTime? GetTokenIssueDate();

        DateTime? GetTokenExpirationDate();

        string GetIssuer();
    }
}
