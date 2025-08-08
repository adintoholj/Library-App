using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class Search : ContentPage
    {
        public ICommand PerformSearchCommand { get; }

        public Search()
        {
            InitializeComponent();

            // stub: wire to your real search logic
            PerformSearchCommand = new Command<string>(query =>
            {
                // TODO: run your search and bind results
            });

            BindingContext = this;
        }

        private async void OnDashboardTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//dashboard");

        private async void OnMemberOperationsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//memberoperations");

        private async void OnBookOperationsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//bookoperations");

        private async void OnSettingsTapped(object sender, EventArgs e)
        {
            // navigate to your SettingsPage
            await Shell.Current.GoToAsync("//settings");
        }

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
