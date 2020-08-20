using System.ComponentModel;
using System.Threading.Tasks;
using Models;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using static Core.Extensions.PositionExtensions;

namespace JobInTown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage
    {
        public ItemDetailPage(object parameter)
            : base(parameter)
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MapPosition")
            {
                MyMap.Pins.Clear();

                if (ViewModel.MapPosition != null)
                {
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = ViewModel.MapPosition,
                        Label = ViewModel.Title,
                        Address = ViewModel.MapAddress
                    };

                    MyMap.Pins.Add(pin);
                }

                Relocate().GetAwaiter();
            }
            else if (e.PropertyName == "CurrentPosition")
            {
                Relocate().GetAwaiter();
            }
        }

        private async Task Relocate()
        {
            if (ViewModel != null && ViewModel.MapPosition != default(Position) && ViewModel.CurrentPosition != default(Position))
            {
                var middlePosition = new Position((ViewModel.MapPosition.Latitude + ViewModel.CurrentPosition.Latitude) / 2, (ViewModel.MapPosition.Longitude + ViewModel.CurrentPosition.Longitude) / 2);
                var distance = ViewModel.MapPosition.DistanceTo(ViewModel.CurrentPosition, UnitOfLength.Kilometers);

                await Task.Delay(2000);

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(middlePosition, Distance.FromKilometers(distance)));
            }
        }
    }
}