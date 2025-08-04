using Microsoft.Maui.Controls;
using System;

namespace BibliothequariaFrontend.Pages
{
    public partial class SplashScreen1 : ContentPage
    {
        public SplashScreen1()
        {
            InitializeComponent();
            // Wire up button click handler
            ProceedButton.Clicked += OnProceedClicked;
        }

        private async void OnProceedClicked(object sender, EventArgs e)
        {
            // Navigate to MainPage (route "main")
            await Shell.Current.GoToAsync("//main");
        }
    }
}