using BibliothequariaFrontend.Controls;
using BibliothequariaFrontend.Models;
using BibliothequariaFrontend.Services;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class MemberOperationsPage : ContentPage
    {
        public ICommand SeeInChargeCommand { get; }
        public ICommand ChangeStatusCommand { get; }
        public ICommand AddUserCommand { get; }

        private readonly MemberService _memberService;

        private List<ClanOverviewDTO> _allMembers = new();           // all members (for popup)
        public ObservableCollection<ClanOverviewDTO> Members { get; } = new();


        public MemberOperationsPage()
        {
            InitializeComponent();

            _memberService = ServiceHelper.GetRequiredService<MemberService>();

            SeeInChargeCommand = new Command(async () =>
            {
                await DisplayAlert("Info", "Not implemented yet.", "OK");
            });

            ChangeStatusCommand = new Command(async () => await ShowChangeStatusPopupAsync());

            // Open the popup and handle the result
            AddUserCommand = new Command(async () => await ShowAddMemberPopupAsync());

            BindingContext = this;
        }

        //call the added collection
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMembersAsync();
        }

        private async Task LoadMembersAsync()
        {
            try
            {
                var list = await _memberService.GetOverviewAsync();
                _allMembers = list ?? new List<ClanOverviewDTO>();   // <-- fill this

                Members.Clear();
                foreach (var m in _allMembers)                        // or filter to active
                    Members.Add(m);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Greška", $"Ne mogu učitati članove.\n{ex.Message}", "OK");
            }
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
                // Todo completed
                await LoadMembersAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Greška", ex.Message, "OK");
            }
        }


        //end of loading real data in the rectangle

        //changing member status 
        private async Task ShowChangeStatusPopupAsync()
        {
            if (_allMembers.Count == 0) await LoadMembersAsync();

            var popup = new ChangeMemberStatusPopup(_allMembers);
            var result = await this.ShowPopupAsync(popup) as ChangeStatusResult;
            if (result is null) return;

            try
            {
                await _memberService.UpdateStatusAsync(result.MemberId, result.Status);
                await LoadMembersAsync();
                await DisplayAlert("Status changed", "Member status updated.", "OK");
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