using Models.Mvvm;

namespace JobInTown.ViewModels
{
    public class ImageDetailViewModel : ViewModelBase
    {
        private string _imageUrl;

        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }

            set
            {
                _imageUrl = value;
                RaisePropertyChanged(() => ImageUrl);
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            ImageUrl = null;

            if (navigationContext is string imageUrl)
            {
                ImageUrl = imageUrl;
            }
        }
    }
}
