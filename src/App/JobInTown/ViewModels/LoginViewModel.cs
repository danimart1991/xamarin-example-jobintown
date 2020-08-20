using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using JobInTown.Azure.Client.Contracts;
using JobInTown.Azure.Client.Models;
using Localization.Contracts;
using Models.Mvvm;
using Navigation.Contracts;
using Network.Models.Exceptions.Basic;
using Settings.Contracts;
using Xamarin.Forms;

namespace JobInTown.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiClient _apiClient;
        private readonly IUserDialogs _userDialogs;
        private readonly ISettingsService _settingsService;
        private readonly ILocalizationService _localizationService;

        private string _email;
        private string _password;

        public LoginViewModel(
            INavigationService navigationService,
            IApiClient apiClient,
            IUserDialogs userDialogs,
            ISettingsService settingsService,
            ILocalizationService localizationService)
        {
            _navigationService = navigationService;
            _apiClient = apiClient;
            _userDialogs = userDialogs;
            _settingsService = settingsService;
            _localizationService = localizationService;
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public ICommand LogInCommand => new Command(async () => await LogIn());

        public ICommand RegisterCommand => new Command(async () => await Register());

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            IsBusy = true;

            await Task.Delay(1000);

            try
            {
                if (_settingsService != null && _settingsService.ContainsKey(GlobalSettings.AccessTokenKey))
                {
                    var token = _settingsService.GetValue<TokenResponse>(GlobalSettings.AccessTokenKey);
                    _apiClient.RefreshLogin(token);
                    await NavigateToMain();
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Ignore
            }
            catch (Exception ex)
            {
#if DEBUG
                await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
#endif
            }
            finally
            {
                IsBusy = false;
            }

            Email = null;
            Password = null;
        }

        private async Task LogIn()
        {
            IsBusy = true;

            await Task.Delay(1000);

            try
            {
                var token = await _apiClient.LogInAsync<TokenResponse>(Email, Password);

                _settingsService.AddOrUpdateValue(GlobalSettings.AccessTokenKey, token);
                _settingsService.AddOrUpdateValue(GlobalSettings.LogedUserNameKey, Email);

                await NavigateToMain();
            }
            catch (NetworkServiceClientErrorException ex)
            {
                await _userDialogs.AlertAsync(ex?.Message);
            }
            catch (Exception ex)
            {
#if DEBUG
                await _userDialogs.AlertAsync($"{ex?.Message}");
#else
                await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
#endif
            }

            IsBusy = false;
        }

        private Task Register()
        {
            return _navigationService.NavigateToAsync<RegisterViewModel>();
        }

        private async Task NavigateToMain()
        {
            await _navigationService.NavigateToAsync<MainViewModel>();
            await _navigationService?.RemoveLastFromBackStackAsync();
        }
    }
}
