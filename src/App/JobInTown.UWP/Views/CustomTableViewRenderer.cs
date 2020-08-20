using JobInTown.UWP.Views;
using JobInTown.Views;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomTableView), typeof(CustomTableViewRenderer))]
namespace JobInTown.UWP.Views
{
    public class CustomTableViewRenderer : TableViewRenderer
    {
    }
}
