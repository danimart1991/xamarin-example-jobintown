using System.Collections.Generic;
using System.Threading.Tasks;
using JobInTown.Azure.Client.Models;
using JobInTown.Azure.Client.Models.Responses;
using Network.Contracts.Models.Interfaces;

namespace JobInTown.Azure.Client.Contracts
{
    public interface IApiClient
    {
        Task RegisterAsync(string email, string password, string confirmPassword, string fullName);

        IToken RefreshLogin(IToken token);

        Task<UserInfo> GetUserInfo();

        Task ChangeImageUrl(string imageUrl);

        Task<TTokenResponse> LogInAsync<TTokenResponse>(string username, string password)
            where TTokenResponse : IToken;

        Task LogoutAsync();

        Task<List<Job>> GetJobsAsync();

        Task<List<Job>> GetMyJobsAsync();

        Task<Job> GetJobAsync(int id);

        Task<Job> PostJobAsync(Job post);

        Task PutJobAsync(int id, Job post);

        Task PatchJobAsync(int id, Job post);

        Task DeleteJobAsync(int id);
    }
}
