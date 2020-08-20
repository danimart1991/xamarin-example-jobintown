using JobInTown.iOS.Views;
using JobInTown.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTableView), typeof(CustomTableViewRenderer))]
namespace JobInTown.iOS.Views
{
    public class CustomTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            Control.SeparatorStyle = UIKit.UITableViewCellSeparatorStyle.None;
        }

    }
}