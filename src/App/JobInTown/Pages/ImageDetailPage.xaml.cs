using Xamarin.Forms.Xaml;

namespace JobInTown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageDetailPage
    {
        public ImageDetailPage(object parameter)
            : base(parameter)
        {
            InitializeComponent();
        }
    }
}