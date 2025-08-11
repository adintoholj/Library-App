using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using BibliothequariaFrontend.Models;
using BibliothequariaFrontend.Services;
using BibliothequariaFrontend.Controls;

namespace BibliothequariaFrontend.Pages
{
    public partial class BookOperations : ContentPage
    {
        // What the list is currently showing
        private enum Showing { None, Available, Borrowed }
        private Showing _showing = Showing.None;

        // Live list bound to the CollectionView
        public ObservableCollection<KnjigaListDTO> Books { get; } = new();

        // Commands for the three buttons
        public ICommand AddBookCommand { get; }
        public ICommand ListAvailableBooksCommand { get; }
        public ICommand ListBorrowedBooksCommand { get; }

        private readonly KnjigaService _bookService;

        public BookOperations()
        {
            InitializeComponent();

            _bookService = ServiceHelper.GetRequiredService<KnjigaService>();

            AddBookCommand = new Command(async () => await ShowAddBookPopupAsync());
            ListAvailableBooksCommand = new Command(async () => await LoadAvailableAsync());
            ListBorrowedBooksCommand = new Command(async () => await LoadBorrowedAsync());

            BindingContext = this;
        }

        private async Task LoadAvailableAsync()
        {
            try
            {
                _showing = Showing.Available;
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
                _showing = Showing.Borrowed;
                var list = await _bookService.GetBorrowedAsync();
                Books.Clear();
                foreach (var b in list) Books.Add(b);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Greška", ex.Message, "OK");
            }
        }

        // Add book
        private async Task ShowAddBookPopupAsync()
        {
            var popup = new AddBookPopup();
            var result = await this.ShowPopupAsync(popup) as AddBookResult;
            if (result is null) return; // user canceled

            try
            {
                var dto = new KnjigaCreateDTO
                {
                    Naslov = result.Naslov,
                    Autor = result.Autor,
                    Zanr = result.Zanr,
                    BrojStrana = result.BrojStrana
                };

                await _bookService.CreateAsync(dto);

                await DisplayAlert("Success", "Book added.", "OK");

                // Refresh current view if needed
                if (_showing == Showing.Available) await LoadAvailableAsync();
                else if (_showing == Showing.Borrowed) await LoadBorrowedAsync();
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
            this.ShowPopup(new ProfileMenuPopup());

        private void OnAvatarTapped(object sender, EventArgs e) =>
            this.ShowPopup(new ProfileMenuPopup());
    }
}
