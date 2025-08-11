using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using BibliothequariaFrontend.Models;

namespace BibliothequariaFrontend.Controls;

public partial class ViewLoanHistoryPopup : Popup
{
    private readonly ObservableCollection<ClanOverviewDTO> _members;
    private readonly Func<int, Task<List<LoanHistoryItem>>> _loadHistoryAsync;

    public ViewLoanHistoryPopup(IEnumerable<ClanOverviewDTO> members,
                                Func<int, Task<List<LoanHistoryItem>>> loadHistoryAsync)
    {
        InitializeComponent();

        _members = new ObservableCollection<ClanOverviewDTO>(members ?? Array.Empty<ClanOverviewDTO>());
        _loadHistoryAsync = loadHistoryAsync;

        MemberPicker.ItemsSource = _members;
        if (_members.Count > 0) MemberPicker.SelectedIndex = 0;

        MemberPicker.SelectedIndexChanged += async (_, __) => await RefreshHistoryAsync();

        // Initial load (Popup has no Loaded event)
        Dispatcher.Dispatch(async () => await RefreshHistoryAsync());
    }

    private async Task RefreshHistoryAsync()
    {
        if (MemberPicker.SelectedItem is not ClanOverviewDTO sel)
        {
            HistoryList.ItemsSource = Array.Empty<LoanHistoryItem>();
            return;
        }

        try
        {
            var items = await _loadHistoryAsync(sel.Id);
            foreach (var i in items)
                i.EnsureComputedDue();

            HistoryList.ItemsSource = items;
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Greška", ex.Message, "OK");
        }
    }

    private void OnClose(object? sender, EventArgs e) => Close();
}
