using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BibliothequariaFrontend.Models;
using BibliothequariaFrontend.Services;

namespace BibliothequariaFrontend.Pages
{
    public partial class Search : ContentPage
    {
        public ICommand PerformSearchCommand { get; }

        private readonly KnjigaService _bookService;
        public ObservableCollection<KnjigaSearchDTO> Results { get; } = new();

        public Search()
        {
            InitializeComponent();

            _bookService = ServiceHelper.GetRequiredService<KnjigaService>();

            PerformSearchCommand = new Command<string>(async _ => await DoSearchAsync());

            BindingContext = this;
        }

        private async Task DoSearchAsync()
        {
            var q = SearchEntry?.Text?.Trim() ?? "";
            if (q.Length < 2)
            {
                Results.Clear();
                return;
            }

            try
            {
                var list = await _bookService.SearchAsync(q);
                Results.Clear();
                foreach (var item in list) Results.Add(item);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Greška", ex.Message, "OK");
            }
        }

        private async void OnSearchClicked(object sender, EventArgs e) => await DoSearchAsync();
        private async void OnSearchCompleted(object sender, EventArgs e) => await DoSearchAsync();

        
        private async void OnDashboardTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//dashboard");

        private async void OnMemberOperationsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//memberoperations");

        private async void OnBookOperationsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//bookoperations");

        private async void OnSettingsTapped(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//settings");

        private void OnProfileTapped(object sender, EventArgs e)
            => this.ShowPopup(new BibliothequariaFrontend.Controls.ProfileMenuPopup());

        private void OnAvatarTapped(object sender, EventArgs e)
            => this.ShowPopup(new BibliothequariaFrontend.Controls.ProfileMenuPopup());
    }
}
