using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class LoginPage : ContentPage
    {
        public ICommand LoginCommand { get; }

        public LoginPage()
        {
            InitializeComponent();

            // Navigate to the dashboard after (stubbed) login
            LoginCommand = new Command(async () =>
            {
                // TODO: validate credentials, call API, then:
                await Shell.Current.GoToAsync("//dashboard");
            });

            BindingContext = this;
        }

        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            // Back to main (or splash) as before
            await Shell.Current.GoToAsync("//main");
        }
    }
}
