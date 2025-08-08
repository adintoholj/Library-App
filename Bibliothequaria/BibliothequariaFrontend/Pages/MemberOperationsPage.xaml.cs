using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BibliothequariaFrontend.Controls;

namespace BibliothequariaFrontend.Pages
{
    public partial class MemberOperationsPage : ContentPage
    {
        public ICommand SeeInChargeCommand { get; }
        public ICommand ChangeStatusCommand { get; }
        public ICommand AddUserCommand { get; }

        public MemberOperationsPage()
        {
            InitializeComponent();

            SeeInChargeCommand = new Command(() => { /* TODO */ });
            ChangeStatusCommand = new Command(() => { /* TODO */ });

            // ⇩ Open the popup and handle the result
            AddUserCommand = new Command(async () => await ShowAddMemberPopupAsync());

            BindingContext = this;
        }

        private async Task ShowAddMemberPopupAsync()
        {
            var popup = new AddMemberPopup();
            var result = await this.ShowPopupAsync(popup) as AddMemberResult;

            if (result is not null)
            {
                // TODO: call your API/EF endpoint to create the member.
                // For now, just confirm.
                await DisplayAlert("Member added",
                    $"{result.FirstName} {result.LastName} was captured.",
                    "OK");
            }
        }

        private async void OnDashboardTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//dashboard");
        }

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
            await Shell.Current.GoToAsync("//settings");
        }

        private void OnProfileTapped(object sender, EventArgs e)
        {
            this.ShowPopup(new ProfileMenuPopup());
        }

        private void OnAvatarTapped(object sender, EventArgs e)
        {
            this.ShowPopup(new ProfileMenuPopup());
        }
    }
}
