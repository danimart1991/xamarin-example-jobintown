using Xamarin.Forms.Xaml;

namespace JobInTown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage
    {
        public AddItemPage(object parameter)
            : base(parameter)
        {
            InitializeComponent();
        }
    }
}
