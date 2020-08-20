using System;
using JobInTown.ViewModels;
using Xamarin.Forms.Xaml;

namespace JobInTown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage
    {
        public RegisterPage(object parameter)
            : base(parameter)
        {
            InitializeComponent();
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            if (ViewModel is RegisterViewModel registerViewModel)
            {
                registerViewModel.RegisterCommand?.Execute(null);
            }
        }
    }
}
