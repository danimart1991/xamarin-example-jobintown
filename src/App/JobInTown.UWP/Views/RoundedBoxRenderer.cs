using System.ComponentModel;
using JobInTown.UWP.Views;
using JobInTown.Views;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(RoundedBox), typeof(RoundedBoxRenderer))]
namespace JobInTown.UWP.Views
{
    public class RoundedBoxRenderer : BoxViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                UpdateCornerRadius(Element as RoundedBox);
                UpdateColor(Element as RoundedBox);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == RoundedBox.CornerRadiusProperty.PropertyName)
            {
                UpdateCornerRadius(Element as RoundedBox);
            }

            if (e.PropertyName == RoundedBox.BackgroundColorProperty.PropertyName)
            {
                UpdateColor(Element as RoundedBox);
            }
        }

        private void UpdateCornerRadius(RoundedBox box)
        {
            Control.RadiusX = (float)box.CornerRadius;
            Control.RadiusY = (float)box.CornerRadius;
        }

        private void UpdateColor(RoundedBox box)
        {
            if (box.BackgroundColor != Color.Transparent)
            {
                var color = Windows.UI.Color.FromArgb(
                (byte)(box.BackgroundColor.A * 255),
                (byte)(box.BackgroundColor.R * 255),
                (byte)(box.BackgroundColor.G * 255),
                (byte)(box.BackgroundColor.B * 255));

                Control.Fill = new SolidColorBrush(color);

                box.BackgroundColor = Color.Transparent;
            }
        }
    }
}