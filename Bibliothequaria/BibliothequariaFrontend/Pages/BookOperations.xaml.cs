using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using BibliothequariaFrontend.Models;
using BibliothequariaFrontend.Services;

namespace BibliothequariaFrontend.Pages
{
    public partial class BookOperations : ContentPage
    {
        // live list bound to the CollectionView
        public ObservableCollection<KnjigaListDTO> Books { get; } = new();

        // commands for the three buttons
        public ICommand AddBookCommand { get; }
        public ICommand ListAvailableBooksCommand { get; }
        public ICommand ListBorrowedBooksCommand { get; }

        private readonly KnjigaService _bookService;

        public BookOperations()
        {
            InitializeComponent();

            _bookService = ServiceHelper.GetRequiredService<KnjigaService>();

            //Create commands first
            AddBookCommand = new Command(async () =>
                await DisplayAlert("Info", "Add book UI coming soon.", "OK"));

            ListAvailableBooksCommand = new Command(async () => await LoadAvailableAsync());
            ListBorrowedBooksCommand = new Command(async () => await LoadBorrowedAsync());

            //Then bind the page
            BindingContext = this;
        }

        private async Task LoadAvailableAsync()
        {
            try
            {
                var list = await _bookService.GetAvailableAsync();
                Books.Clear();
                foreach (var b in list) Books.Add(b);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Greška", ex.Message, "OK");
            }
        }

        private async Task LoadBorrowedAsync()
        {
            try
            {
                var list = await _bookService.GetBorrowedAsync();
                Books.Clear();
                foreach (var b in list) Books.Add(b);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Greška", ex.Message, "OK");
            }
        }

        // NAV
        private async void OnDashboardTapped(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("//dashboard");

        private async void OnMemberOperationsTapped(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("//memberoperations");

        private async void OnSearchTapped(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("//search");

        private async void OnSettingsTapped(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("//settings");

        private void OnProfileTapped(object sender, EventArgs e) =>
            this.ShowPopup(new BibliothequariaFrontend.Controls.ProfileMenuPopup());

        private void OnAvatarTapped(object sender, EventArgs e) =>
            this.ShowPopup(new BibliothequariaFrontend.Controls.ProfileMenuPopup());
    }
}
