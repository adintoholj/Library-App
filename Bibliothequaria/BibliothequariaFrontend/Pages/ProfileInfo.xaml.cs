using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class ProfileInfo : ContentPage
    {
        public ICommand SaveCommand { get; }

        public ProfileInfo()
        {
            InitializeComponent();

            // Hook up your save logic here
            SaveCommand = new Command(OnSaveChanges);
            BindingContext = this;
        }

        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            // Navigate back one step
            await Shell.Current.GoToAsync("//dashboard");
        }

        private async void OnSaveChanges()
        {
            // TODO: persist changes
            await DisplayAlert("Profile", "Your changes have been saved.", "OK");
        }
    }
}
