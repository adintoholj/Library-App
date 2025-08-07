using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class RegisterPage : ContentPage
    {
        public ICommand RegisterCommand { get; }

        public RegisterPage()
        {
            InitializeComponent();

            // Navigate directly to the dashboard after registration
            RegisterCommand = new Command(async () =>
            {
                // TODO: collect Entry.Text values, call your API, then:
                await Shell.Current.GoToAsync("//dashboard");
            });

            BindingContext = this;
        }

        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            // Back to main (or splash)
            await Shell.Current.GoToAsync("//main");
        }
    }
}
