using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Storage;

namespace BibliothequariaFrontend.Pages
{
    public partial class Settings : ContentPage
    {
        const string KDark = "DarkMode";
        public Settings()
        {
            InitializeComponent();
            DarkModeSwitch.IsToggled = Preferences.Default.Get(KDark, false); // read on load
        }

        void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            Preferences.Default.Set("DarkMode", e.Value);

            var res = Application.Current.Resources;
            // background you chose
            res["ContentBackground"] = e.Value ? Color.FromArgb("#3B3838") : Colors.White;
            // header text: white on dark, black on light
            res["ContentTextColor"] = e.Value ? Colors.White : Color.FromArgb("#1A1A1A");
        }



        /*protected override void OnAppearing()
        {
            base.OnAppearing();
            AccentPicker.SelectedItem = Preferences.Default.Get("ThemeAccent", "Amber");
        }*/

        /*void OnAccentChanged(object sender, EventArgs e)
        {
            if (AccentPicker.SelectedItem is not string choice) return;
            Preferences.Default.Set("ThemeAccent", choice);

            // Apply now
            var svc = ServiceHelper.GetRequiredService<BibliothequariaFrontend.Services.ISettingsService>();
            svc.Initialize(); // or call ApplyAccent(choice) if you expose it
        }*/

        // NAVIGATION HANDLERS
        private async void OnDashboardTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//dashboard");

        private async void OnMemberOperationsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//memberoperations");

        private async void OnBookOperationsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//bookoperations");

        private async void OnSearchTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//search");

        private async void OnSettingsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//settings");
    }
}
