using Autofac;
using Models.Mvvm;
using Xamarin.Forms;

namespace JobInTown.Pages.Base
{
    public class TabbedPageBase<TViewModel> : TabbedPage, IPageBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        private readonly TViewModel _viewModel;

        public TabbedPageBase(object parameter)
            : this()
        {
            Parameter = parameter;
        }

        public TabbedPageBase()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _viewModel = AppContainer.Container.Resolve<TViewModel>();
            }

            BindingContext = _viewModel;
        }

        public TViewModel ViewModel
        {
            get { return _viewModel; }
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel?.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel?.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return ViewModel?.OnBackButtonPressed(base.OnBackButtonPressed()) ?? base.OnBackButtonPressed();
        }
    }
}