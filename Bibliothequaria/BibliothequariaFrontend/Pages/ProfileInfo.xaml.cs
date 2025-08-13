using Microsoft.Maui.Controls;
using System;
using System.Net.Http;                    // for new HttpClient()
using System.Windows.Input;
using BibliothequariaFrontend.Services;   // AuthService

namespace BibliothequariaFrontend.Pages
{
    public partial class ProfileInfo : ContentPage
    {
        private readonly AuthService _auth;

        // Use this if your XAML binds a Button's Command="{Binding SaveCommand}"
        public ICommand SaveCommand { get; }

        // DI constructor (used when the page is resolved via dependency injection)
        public ProfileInfo(AuthService auth)
        {
            InitializeComponent();
            _auth = auth;

            SaveCommand = new Command(OnSaveChanges);
            BindingContext = this;
        }

        // Default constructor (used when Shell creates the page from XAML)
        // Forwards to the DI constructor with a fallback HttpClient.
        public ProfileInfo() : this(new AuthService(new HttpClient()))
        {
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // read current user id from Preferences (set during Login/Register)
            var id = Preferences.Default.Get("CurrentUserId", 0);
            if (id == 0) return;

            var u = await _auth.GetProfileAsync(id);
            if (u == null) return;

            // these names must exist in ProfileInfo.xaml
            NameEntry.Text = u.Ime;
            SurnameEntry.Text = u.Prezime;
            EmailLabel.Text = u.EMail;          // read-only entry/label
            PhoneEntry.Text = u.Telefon ?? "";
        }

        // Command target (no parameters)
        private async void OnSaveChanges()
        {
            var id = Preferences.Default.Get("CurrentUserId", 0);
            if (id == 0) return;

            var ok = await _auth.UpdateProfileAsync(
                id,
                NameEntry.Text?.Trim(),
                SurnameEntry.Text?.Trim(),
                PhoneEntry.Text?.Trim()
            );

            await DisplayAlert("Profile", ok ? "Saved." : "Failed to save.", "OK");
        }

        // If your XAML uses Clicked="OnSaveChanges", keep this wrapper too:
        private void OnSaveChanges(object sender, EventArgs e) => OnSaveChanges();

        private async void OnBackArrowTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//dashboard");
    }
}
