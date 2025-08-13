using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;
using BibliothequariaFrontend.Services;  // <—
using BibliothequariaFrontend.Models;

namespace BibliothequariaFrontend.Pages
{
    // ===== REGISTER (START) =====
    public partial class RegisterPage : ContentPage
    {
        private readonly AuthService _auth;
        public RegisterPage(AuthService auth)
        {
            InitializeComponent();
            _auth = auth;
        }


        public RegisterPage() : this(new AuthService(new HttpClient())) { }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var dto = new RegisterRadnikDTO
            {
                Ime = NameEntry.Text?.Trim() ?? "",
                Prezime = SurnameEntry.Text?.Trim() ?? "",
                EMail = EmailEntry.Text?.Trim() ?? "",
                Password = PasswordEntry.Text ?? "",
            };

            var user = await _auth.RegisterAsync(dto);
            if (user == null)
                await DisplayAlert("Register", "Failed to register.", "OK");
            else
                await Shell.Current.GoToAsync("//dashboard");
        }

// ===== REGISTER (END) =====


        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            // Back to main (or splash)
            await Shell.Current.GoToAsync("//main");
        }
    }
}
