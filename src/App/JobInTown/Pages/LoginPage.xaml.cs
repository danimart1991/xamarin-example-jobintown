using System;
using JobInTown.ViewModels;
using Xamarin.Forms.Xaml;

namespace JobInTown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        public LoginPage(object parameter)
            : base(parameter)
        {
            InitializeComponent();
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            if (ViewModel is LoginViewModel loginViewModel)
            {
                loginViewModel.LogInCommand?.Execute(null);
            }
        }
    }
}