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

            // Stubbed command: replace with real login logic
            LoginCommand = new Command(async () =>
            {
                // TODO: validate credentials, call API, then...
                await Shell.Current.GoToAsync("//main");
            });

            BindingContext = this;
        }

        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }
    }
}
