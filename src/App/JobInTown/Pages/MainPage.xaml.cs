using System;
using Localization.Contracts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobInTown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        private bool _newOrder;

        public MainPage(object parameter)
            : base(parameter)
        {
            InitializeComponent();
        }

        private void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void OnRefreshToolbarItemClicked(object sender, EventArgs e)
        {
            if (CurrentPage == JobsPage)
            {
                ViewModel?.GetItemsCommand?.Execute(null);
            }
            else if (CurrentPage == ProfilePage)
            {
                ViewModel?.GetMyThingsCommand?.Execute(null);
            }
        }

        private void OnOrderToolbarItemClicked(object sender, EventArgs e)
        {
            var localizationService = AppContainer.Resolve<ILocalizationService>();
            if (ViewModel != null && localizationService != null)
            {
                if (!_newOrder)
                {
                    var distanceOrder = localizationService?.GetString("MainPage_NameOrder_ToolBarItem_Text");
                    OrderToolbarItem.Text = distanceOrder;

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Windows:
                        case Device.WinPhone:
                            OrderToolbarItem.Icon = "Assets/Icons/OrderNameIcon.png";
                            break;
                        case Device.iOS:
                            OrderToolbarItem.Icon = "Icons/OrderNameIcon.png";
                            break;
                        case Device.Android:
                            OrderToolbarItem.Icon = "ic_text_format_white_24dp.png";
                            break;
                        default:
                            OrderToolbarItem.Icon = "OrderNameIcon.png";
                            break;
                    }

                    ViewModel.OrderJobsByDistanceCommand?.Execute(null);

                    _newOrder = true;
                }
                else
                {
                    var dateOrder = localizationService?.GetString("MainPage_DateOrder_ToolBarItem_Text");
                    OrderToolbarItem.Text = dateOrder;

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Windows:
                        case Device.WinPhone:
                            OrderToolbarItem.Icon = "Assets/Icons/OrderDateIcon.png";
                            break;
                        case Device.iOS:
                            OrderToolbarItem.Icon = "Icons/OrderDateIcon.png";
                            break;
                        case Device.Android:
                            OrderToolbarItem.Icon = "ic_access_time_white_24dp.png";
                            break;
                        default:
                            OrderToolbarItem.Icon = "OrderDateIcon.png";
                            break;
                    }

                    ViewModel.OrderJobsByDateCommand?.Execute(null);

                    _newOrder = false;
                }
            }
        }
    }
}
