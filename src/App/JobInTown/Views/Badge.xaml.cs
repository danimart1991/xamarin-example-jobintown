using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobInTown.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Badge : ContentView
    {
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(double), typeof(Badge), 16d);

        public static readonly new BindableProperty BackgroundColorProperty =
            BindableProperty.Create("BackgroundColor", typeof(Color), typeof(Badge), Color.Default);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(Badge), "Badge");

        public Badge()
        {
            InitializeComponent();
        }

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == FontSizeProperty.PropertyName)
            {
                ViewLabel.FontSize = FontSize;
                ViewGrid.HeightRequest = FontSize * 1.8d;
                ViewRoundedBox.CornerRadius = FontSize / 2d;
                ViewLabel.Margin = new Thickness(FontSize / 2d, 0);
            }

            if (propertyName == BackgroundColorProperty.PropertyName)
            {
                ViewRoundedBox.BackgroundColor = BackgroundColor;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                ViewLabel.Text = Text;
            }
        }
    }
}