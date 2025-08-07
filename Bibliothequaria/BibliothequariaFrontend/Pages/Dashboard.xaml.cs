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
            // Assumes you've registered the "memberoperations" route in AppShell.xaml
            await Shell.Current.GoToAsync("//memberoperations");
        }
    }
}
