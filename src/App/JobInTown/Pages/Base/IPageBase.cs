using Models.Mvvm;

namespace JobInTown.Pages.Base
{
    internal interface IPageBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        TViewModel ViewModel { get; }

        object Parameter { get; set; }
    }
}