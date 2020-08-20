using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Mvvm;
using Navigation.Contracts;
using Xamarin.Forms;

namespace Navigation
{
    public class NavigationService : INavigationService
    {
        private Dictionary<Type, Type> _mappings;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
        }

        public Dictionary<Type, Type> Mappings
        {
            get { return _mappings; }
            set { _mappings = value; }
        }

        protected Application CurrentApplication => Application.Current;

        public Task NavigateToAsync<TViewModel>()
            where TViewModel : IViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter)
            where TViewModel : IViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public Task NavigateBackAsync()
        {
            return CurrentApplication.MainPage?.Navigation?.PopAsync(true);
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            if (CurrentApplication.MainPage is NavigationPage mainPage && mainPage.Navigation.NavigationStack.Count > 1)
            {
                mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public virtual Task RemoveBackStackAsync()
        {
            while (CurrentApplication.MainPage is NavigationPage mainPage && mainPage.Navigation.NavigationStack.Count > 1)
            {
                mainPage.Navigation.RemovePage(mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (CurrentApplication.MainPage is NavigationPage navigationPage)
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Windows:
                    case Device.WinPhone:
                        await navigationPage.PushAsync(page, false);
                        break;
                    default:
                        await navigationPage.PushAsync(page, true);
                        break;
                }
            }
            else
            {
                CurrentApplication.MainPage = new NavigationPage(page)
                {
                    BarBackgroundColor = Color.FromHex("#DD9944"),
                    BarTextColor = Color.White,
                    Title = "JobInTown"
                };
            }
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object navigationContext = null)
        {
            Page page = null;

            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            page = Activator.CreateInstance(pageType, navigationContext) as Page;

            return page;
        }
    }
}