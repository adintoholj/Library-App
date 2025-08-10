using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using BibliothequariaFrontend.Models;

namespace BibliothequariaFrontend.Controls;

public partial class ChangeMemberStatusPopup : Popup
{
    private readonly ObservableCollection<ClanOverviewDTO> _members;

    public ChangeMemberStatusPopup(IEnumerable<ClanOverviewDTO> members)
    {
        InitializeComponent();

        _members = new ObservableCollection<ClanOverviewDTO>(members ?? Array.Empty<ClanOverviewDTO>());
        MemberPicker.ItemsSource = _members;

        // Preselect first member if available so text is visible immediately
        if (_members.Count > 0)
            MemberPicker.SelectedIndex = 0;

        StatusSwitch.IsToggled = (_members.Count > 0) ? (_members[0].Status) : false; //CAHNGED ??
        UpdateStatusText();

        MemberPicker.SelectedIndexChanged += (_, __) =>
        {
            if (MemberPicker.SelectedItem is ClanOverviewDTO sel)
                StatusSwitch.IsToggled = sel.Status; // reflect current status
            UpdateStatusText();
        };
    }

    private void OnToggled(object? sender, ToggledEventArgs e) => UpdateStatusText();

    private void UpdateStatusText() =>
        StatusText.Text = StatusSwitch.IsToggled ? "Active" : "Inactive";

    private void OnCancel(object? sender, EventArgs e) => Close();

    private void OnSave(object? sender, EventArgs e)
    {
        if (MemberPicker.SelectedItem is not ClanOverviewDTO sel)
        {
            ErrorLabel.IsVisible = true;
            return;
        }

        // return the selection to the page
        Close(new ChangeStatusResult(sel.Id, StatusSwitch.IsToggled));
    }
}

public record ChangeStatusResult(int MemberId, bool Status);
