using Android.Graphics.Drawables;
using JobInTown.Droid.Views;
using JobInTown.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CustomTableView), typeof(CustomTableViewRenderer))]
namespace JobInTown.Droid.Views
{
    public class CustomTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            Control.Divider = new ColorDrawable(Color.Transparent);
            Control.DividerHeight = 0;
        }
    }
}