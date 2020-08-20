using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureStorage.Contracts;
using JobInTown.Pages;
using JobInTown.ViewModels;
using Localization.Contracts;
using Navigation.Contracts;
using Xamarin.Forms;

namespace JobInTown
{
    public partial class App : Application
    {
        public App(AppSetup setup)
        {
            InitializeComponent();

            AppContainer.Container = setup.CreateContainer();

            InitAzureStorageService();
            InitLocalization();
            InitNavigation();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private Task InitNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            navigationService.Mappings = CreatePageViewModelMappings();

            return navigationService.NavigateToAsync<LoginViewModel>();
        }

        private Dictionary<Type, Type> CreatePageViewModelMappings()
        {
            var mappings = new Dictionary<Type, Type>
            {
                { typeof(LoginViewModel), typeof(LoginPage) },
                { typeof(MainViewModel), typeof(MainPage) },
                { typeof(ItemDetailViewModel), typeof(ItemDetailPage) },
                { typeof(AddItemViewModel), typeof(AddItemPage) },
                { typeof(RegisterViewModel), typeof(RegisterPage) },
                { typeof(ImageDetailViewModel), typeof(ImageDetailPage) }
            };

            return mappings;
        }

        private void InitLocalization()
        {
            var localizationService = AppContainer.Resolve<ILocalizationService>();
            if (localizationService != null)
            {
                var cultureInfo = localizationService.GetCurrentCultureInfo();
                Resx.AppResources.Culture = cultureInfo;
                localizationService.SetLocale(cultureInfo);
                localizationService.ResourceManager = Resx.AppResources.ResourceManager;
            }
        }

        private void InitAzureStorageService()
        {
            var azureStorageService = AppContainer.Resolve<IAzureStorageService>();
            if (azureStorageService != null)
            {
                azureStorageService.ConnectionString = GlobalSettings.AzureStorageConnectionString;
                azureStorageService.ContainerName = GlobalSettings.AzureStorageContainerName;
            }
        }
    }
}
