using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;
using BibliothequariaFrontend.Services;  
using BibliothequariaFrontend.Models;

namespace BibliothequariaFrontend.Pages
{
    // ===== LOGIN (START) =====
    public partial class LoginPage : ContentPage
    {
        private readonly AuthService _auth;

        public ICommand LoginCommand { get; }
        // DI ctor
        public LoginPage(AuthService auth)
        {
            InitializeComponent();
            _auth = auth;

            LoginCommand = new Command(async () => await DoLoginAsync());
            BindingContext = this;               // <-- so {Binding LoginCommand} resolves
        }

        //default constructor
        public LoginPage() : this(new AuthService(new HttpClient()))
        {
            // fallback instance
        }

        private async Task DoLoginAsync()
        {
            var email = EmailEntry.Text?.Trim() ?? "";
            var pass = PasswordEntry.Text ?? "";

            var user = await _auth.LoginAsync(email, pass);
            if (user == null)
                await DisplayAlert("Login", "Invalid e-mail or password.", "OK");
            else
                await Shell.Current.GoToAsync("//dashboard");
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = EmailEntry.Text?.Trim() ?? "";
            var pass = PasswordEntry.Text ?? "";
            var user = await _auth.LoginAsync(email, pass);

            if (user == null)
                await DisplayAlert("Login", "Invalid e-mail or password.", "OK");
            else
                await Shell.Current.GoToAsync("//dashboard");
        }
// ===== LOGIN (END) =====


        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            // Back to main (or splash) as before
            await Shell.Current.GoToAsync("//main");
        }


    }
}
