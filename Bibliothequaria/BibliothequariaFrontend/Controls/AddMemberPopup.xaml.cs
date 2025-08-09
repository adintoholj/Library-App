using CommunityToolkit.Maui.Views;

namespace BibliothequariaFrontend.Controls
{
    public partial class AddMemberPopup : Popup
    {
        public AddMemberPopup()
        {
            InitializeComponent();
        }

        private void OnCancel(object sender, EventArgs e) => Close();

        private void OnAdd(object sender, EventArgs e)
        {
            var first = FirstNameEntry.Text?.Trim();
            var last = LastNameEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            {
                ErrorLabel.IsVisible = true;
                return;
            }

            Close(new AddMemberResult(first!, last!));
        }
    }

    public record AddMemberResult(string FirstName, string LastName);
}