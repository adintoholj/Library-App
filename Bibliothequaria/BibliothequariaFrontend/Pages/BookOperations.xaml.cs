using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;

namespace BibliothequariaFrontend.Pages
{
    public partial class BookOperations : ContentPage
    {
        public BookOperations()
        {
            InitializeComponent();
            // … your existing command‐initialization, if any …
        }

        private async void OnDashboardTapped(object sender, EventArgs e)
        {
            // navigate back to Dashboard
            await Shell.Current.GoToAsync("//dashboard");
        }

        private async void OnMemberOperationsTapped(object sender, EventArgs e)
        {
            // navigate to MemberOperations
            await Shell.Current.GoToAsync("//memberoperations");
        }

        //navigate to search
        private async void OnSearchTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//search");
        }

        //nav to settings
        private async void OnSettingsTapped(object sender, EventArgs e)
        {
            // navigate to your SettingsPage
            await Shell.Current.GoToAsync("//settings");
        }

        //profile popup
        private void OnProfileTapped(object sender, EventArgs e)
        {
            // Show the popup anchored to the current page
            this.ShowPopup(new BibliothequariaFrontend.Controls.ProfileMenuPopup());
        }

        private void OnAvatarTapped(object sender, EventArgs e)
        {

            this.ShowPopup(new BibliothequariaFrontend.Controls.ProfileMenuPopup());
        }
    }
}
