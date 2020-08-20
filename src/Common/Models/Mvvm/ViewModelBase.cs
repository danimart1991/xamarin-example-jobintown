namespace Models.Mvvm
{
    public class ViewModelBase : ExtendedBindableObject, IViewModel
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public virtual void OnAppearing(object navigationContext)
        {
        }

        public virtual void OnDisappearing()
        {
        }

        public virtual bool OnBackButtonPressed(bool stopBackRequest)
        {
            return stopBackRequest;
        }
    }
}
