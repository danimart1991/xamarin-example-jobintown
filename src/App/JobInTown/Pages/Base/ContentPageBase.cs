using Autofac;
using Models.Mvvm;
using Xamarin.Forms;

namespace JobInTown.Pages.Base
{
    public class ContentPageBase<TViewModel> : ContentPage, IPageBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        private readonly TViewModel _viewModel;

        public ContentPageBase(object parameter)
            : this()
        {
            Parameter = parameter;
        }

        public ContentPageBase()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _viewModel = AppContainer.Container.Resolve<TViewModel>();
            }

            BindingContext = _viewModel;
        }

        public TViewModel ViewModel => _viewModel;

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            ViewModel?.OnAppearing(Parameter);

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            ViewModel?.OnDisappearing();

            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return ViewModel?.OnBackButtonPressed(base.OnBackButtonPressed()) ?? base.OnBackButtonPressed();
        }
    }
}
