using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class RegisterPage : ContentPage
    {
        // Simple ICommand stub – hook this up to your real register logic or ViewModel
        public ICommand RegisterCommand { get; }

        public RegisterPage()
        {
            InitializeComponent();

            // If you have a ViewModel, set BindingContext = new YourRegisterViewModel();
            // For now, bind RegisterCommand to a stub:
            RegisterCommand = new Command(OnRegister);
            BindingContext = this;
        }

        private async void OnRegister()
        {
            // TODO: collect Entry.Text values and call your API
            await DisplayAlert("Register", "Register button tapped", "OK");
        }

        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            // Navigate back to main or splash
            await Shell.Current.GoToAsync("//main");
        }
    }
}
