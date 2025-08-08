using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace BibliothequariaFrontend.Controls
{
    public partial class ProfileMenuPopup : Popup
    {
        public ProfileMenuPopup()
        {
            InitializeComponent();
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
