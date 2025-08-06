using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class MainPage : ContentPage
    {
        // Already-existing create‐account command
        public ICommand CreateAccountCommand { get; }
        // New login command
        public ICommand LoginCommand { get; }

        public MainPage()
        {
            InitializeComponent();

            // Navigate to the register page
            CreateAccountCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//register");
            });

            // Navigate to the login page
            LoginCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//login");
            });

            // Make both commands available to your XAML bindings
            BindingContext = this;
        }

        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//splash");
        }
    }
}
