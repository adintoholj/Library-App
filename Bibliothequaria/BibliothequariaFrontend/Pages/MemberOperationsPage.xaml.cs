using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BibliothequariaFrontend.Controls;
using BibliothequariaFrontend.Models;
using BibliothequariaFrontend.Services;
using System.Net.Http.Json;

namespace BibliothequariaFrontend.Pages
{
    public partial class MemberOperationsPage : ContentPage
    {
        public ICommand SeeInChargeCommand { get; }
        public ICommand ChangeStatusCommand { get; }
        public ICommand AddUserCommand { get; }

        private readonly MemberService _memberService;

        public MemberOperationsPage()
        {
            InitializeComponent();

            _memberService = ServiceHelper.GetRequiredService<MemberService>();

            SeeInChargeCommand = new Command(async () =>
            {
                await DisplayAlert("Info", "Not implemented yet.", "OK");
            });

            ChangeStatusCommand = new Command(async () =>
            {
                await DisplayAlert("Info", "Not implemented yet.", "OK");
            });

            // Open the popup and handle the result
            AddUserCommand = new Command(async () => await ShowAddMemberPopupAsync());

            BindingContext = this;
        }


        private async Task ShowAddMemberPopupAsync()
        {
            var popup = new AddMemberPopup();
            var result = await this.ShowPopupAsync(popup) as AddMemberResult;

            if (result is null) return;

            var dto = new ClanCreateDTO
            {
                Ime = result.FirstName,
                Prezime = result.LastName,
                DatumUclane = DateOnly.FromDateTime(DateTime.Now)
            };

            try
            {
                

                // Make the call
                var created = await _memberService.CreateAsync(dto);

                await DisplayAlert("Member added",
                    $"{created.Ime} {created.Prezime} (ID {created.Id}) added.",
                    "OK");

                // TODO: trigger a members list refresh here if you have one
            }
            catch (Exception ex)
            {
                await DisplayAlert("Greška", ex.Message, "OK");
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
