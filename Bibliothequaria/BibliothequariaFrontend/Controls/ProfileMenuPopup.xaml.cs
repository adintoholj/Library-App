using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace BibliothequariaFrontend.Controls
{
    public partial class ProfileMenuPopup : Popup
    {
        public ProfileMenuPopup()
        {
            InitializeComponent();

            // pull from saved session
            var name = Preferences.Default.Get("CurrentUserName", "");
            var email = Preferences.Default.Get("CurrentUserEmail", "");

            if (!string.IsNullOrWhiteSpace(name))
                PopupNameLabel.Text = name;     // must match x:Name in XAML

            if (!string.IsNullOrWhiteSpace(email))
                PopupEmailLabel.Text = email;   // must match x:Name in XAML
        }

        private async void OnMoreInfoClicked(object sender, EventArgs e)
        {
            // Navigate to your detailed profile page
            await Shell.Current.GoToAsync("//profileinfo");
            Close();   // dismiss the popup
        }

        private async void OnBackArrowTapped(object sender, EventArgs e)
        {
            // pops back to the previous page in the same ShellSection
            await Shell.Current.GoToAsync("..");
        }

    }
}
