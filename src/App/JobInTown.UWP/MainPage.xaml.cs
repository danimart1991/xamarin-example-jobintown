using FFImageLoading.Forms.WinUWP;
using Windows.Services.Maps;

namespace JobInTown.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Xamarin.FormsMaps.Init(GlobalSettings.WindowsMapToken);
            MapService.ServiceToken = GlobalSettings.WindowsMapToken;
            CachedImageRenderer.Init();

            LoadApplication(new JobInTown.App(new Setup()));
        }
    }
}
