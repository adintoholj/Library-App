using CommunityToolkit.Maui.Views;

namespace BibliothequariaFrontend.Controls;

public partial class AddBookPopup : Popup
{
    public AddBookPopup()
    {
        InitializeComponent();
    }

    private void OnCancel(object? sender, EventArgs e) => Close();

    private async void OnSave(object? sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var title = TitleEntry.Text?.Trim();
        var author = AuthorEntry.Text?.Trim();
        var genre = GenreEntry.Text?.Trim();
        var pagesT = PagesEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(title) ||
            string.IsNullOrWhiteSpace(author) ||
            string.IsNullOrWhiteSpace(genre) ||
            !int.TryParse(pagesT, out var pages) || pages <= 0)
        {
            ErrorLabel.Text = "Please fill all fields. Pages must be a positive number.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Optional: quick debug to confirm the handler fires
        // await Application.Current.MainPage.DisplayAlert("Debug", "Returning popup result…", "OK");

        Close(new AddBookResult(title, author, genre, pages));
    }
}

public record AddBookResult(string Naslov, string Autor, string Zanr, int BrojStrana);
