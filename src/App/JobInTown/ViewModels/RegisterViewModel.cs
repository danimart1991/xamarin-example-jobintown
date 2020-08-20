using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using JobInTown.Azure.Client.Contracts;
using JobInTown.Azure.Client.Models;
using Localization.Contracts;
using Models.Mvvm;
using Navigation.Contracts;
using Network.Models.Exceptions.Basic;
using Xamarin.Forms;

namespace JobInTown.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiClient _apiClient;
        private readonly IUserDialogs _userDialogs;
        private readonly ILocalizationService _localizationService;

        private string _fullName;
        private string _email;
        private string _password;
        private string _confirmPassword;

        public RegisterViewModel(
            INavigationService navigationService,
            IApiClient apiClient,
            IUserDialogs userDialogs,
            ILocalizationService localizationService)
        {
            _navigationService = navigationService;
            _apiClient = apiClient;
            _userDialogs = userDialogs;
            _localizationService = localizationService;
        }

        public string FullName
        {
            get
            {
                return _fullName;
            }

            set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
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

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }

            set
            {
                _confirmPassword = value;
                RaisePropertyChanged(() => ConfirmPassword);
            }
        }

        public ICommand RegisterCommand => new Command(async () => await Register(), () => { return !IsBusy; });

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            FullName = null;
            Email = null;
            Password = null;
            ConfirmPassword = null;
        }

        private async Task Register()
        {
            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    await _apiClient.RegisterAsync(Email, Password, ConfirmPassword, FullName);

                    await _userDialogs.AlertAsync(string.Format(_localizationService.GetString("RegisterPage_Welcome_Message"), FullName));

                    await _navigationService.NavigateBackAsync();
                }
                catch (NetworkServiceClientErrorException ex)
                {
                    await _userDialogs.AlertAsync(ex?.Message, ((RegisterError)ex?.Error)?.Message);
                }
                catch
                {
                    await _userDialogs.AlertAsync(_localizationService.GetString("Exception_General_Message"));
                }

                IsBusy = false;
            }
        }
    }
}