using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Mvvm;

namespace Navigation.Contracts
{
    public interface INavigationService
    {
        Dictionary<Type, Type> Mappings { get; set; }

        Task NavigateToAsync<TViewModel>()
            where TViewModel : IViewModel;

        Task NavigateToAsync<TViewModel>(object parameter)
            where TViewModel : IViewModel;

        Task NavigateToAsync(Type viewModelType);

        Task NavigateToAsync(Type viewModelType, object parameter);

        Task NavigateBackAsync();

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
