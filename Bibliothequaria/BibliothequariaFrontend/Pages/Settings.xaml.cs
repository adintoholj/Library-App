using Microsoft.Maui.Controls;
using System;

namespace BibliothequariaFrontend.Pages
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

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
