using Microsoft.Maui.Controls;
using System;

namespace BibliothequariaFrontend.Pages
{
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private async void OnMemberOperationsTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//memberoperations");
        }

        // ⇩ New handler ⇩
        private async void OnBookOperationsTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//bookoperations");
        }

        private async void OnSearchTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//search");
        }

        private async void OnSettingsTapped(object sender, EventArgs e)
        {
            // navigate to your SettingsPage
            await Shell.Current.GoToAsync("//settings");
        }

    }
}
