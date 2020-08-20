using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobInTown.Azure.Client.Contracts;
using JobInTown.Azure.Client.Models;
using JobInTown.Azure.Client.Models.Requests;
using JobInTown.Azure.Client.Models.Responses;
using Network.Contracts;
using Network.Contracts.Models.Interfaces;
using Network.Models;

namespace JobInTown.Azure.Client
{
    public class ApiClient : IApiClient
    {
        private const string BaseUri = "http://YOURAPPSERVICE.azurewebsites.net/";
        private const string JobsRouteUri = "api/Jobs";
        private const string MyJobsRouteUri = "api/My/Jobs";
        private const string GetUserInfoRouteUri = "api/Account/UserInfo";
        private const string RegisterRouteUri = "api/Account/Register";
        private const string ChangeImageUrlRouteUri = "api/Account/ChangeImageUrl";
        private const string LogoutRouteUri = "api/Account/Logout";
        private const string TokenRouteUri = "Token";

        private readonly INetworkService _networkService;

        private Uri _baseUri;

        public ApiClient(INetworkService networkService)
        {
            _networkService = networkService;

            _baseUri = new Uri(BaseUri);
        }

        public Task RegisterAsync(string email, string password, string confirmpassword, string fullName)
        {
            var requestUri = $"{BaseUri}{RegisterRouteUri}";

            var registerUser = new RegisterUser()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmpassword,
                FullName = fullName
            };

            return _networkService.Post<RegisterUser, RegisterError>(requestUri, registerUser);
        }

        public IToken RefreshLogin(IToken token)
        {
            return _networkService.InitToken(token);
        }

        public Task<UserInfo> GetUserInfo()
        {
            var requestUri = $"{BaseUri}{GetUserInfoRouteUri}";

            return _networkService.Get<UserInfo, ErrorModel>(requestUri);
        }

        public Task ChangeImageUrl(string imageUrl)
        {
            var requestUri = $"{BaseUri}{ChangeImageUrlRouteUri}";

            var changeImageRequest = new ChangeImageRequest()
            {
                ImageUrl = imageUrl
            };

            return _networkService.Post<ChangeImageRequest, ErrorModel>(requestUri, changeImageRequest);
        }

        public Task<TTokenResponse> LogInAsync<TTokenResponse>(string username, string password)
            where TTokenResponse : IToken
        {
            var requestUri = $"{BaseUri}{TokenRouteUri}";

            var configurationParameters = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", username },
                { "password", password }
            };

            return _networkService.GetAuthToken<TTokenResponse, LoginError>(requestUri, configurationParameters);
        }

        public Task LogoutAsync()
        {
            var requestUri = $"{BaseUri}{LogoutRouteUri}";

            _networkService.RemoveToken();

            return _networkService.Post<ErrorModel>(requestUri);
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            var requestUri = $"{BaseUri}/{JobsRouteUri}";
            return await _networkService.Get<List<Job>, ErrorModel>(requestUri);
        }

        public async Task<List<Job>> GetMyJobsAsync()
        {
            var requestUri = $"{BaseUri}/{MyJobsRouteUri}";
            return await _networkService.Get<List<Job>, ErrorModel>(requestUri);
        }

        public async Task<Job> GetJobAsync(int id)
        {
            var requestUri = $"{BaseUri}/{JobsRouteUri}/{id}";

            return await _networkService.Get<Job, ErrorModel>(requestUri);
        }

        public async Task<Job> PostJobAsync(Job post)
        {
            var requestUri = $"{BaseUri}/{JobsRouteUri}";
            return await _networkService.Post<Job, Job, ErrorModel>(requestUri, post);
        }

        public Task PutJobAsync(int id, Job post)
        {
            throw new NotImplementedException();
        }

        public Task PatchJobAsync(int id, Job post)
        {
            throw new NotImplementedException();
        }

        public Task DeleteJobAsync(int id)
        {
            var requestUri = $"{BaseUri}/{JobsRouteUri}/{id}";

            return _networkService.Delete<ErrorModel>(requestUri);
        }
    }
}
