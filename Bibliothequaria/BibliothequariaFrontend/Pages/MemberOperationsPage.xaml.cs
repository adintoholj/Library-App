using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class MemberOperationsPage : ContentPage
    {
        public ICommand SeeInChargeCommand { get; }
        public ICommand ChangeStatusCommand { get; }
        public ICommand AddUserCommand { get; }

        public MemberOperationsPage()
        {
            InitializeComponent();

            SeeInChargeCommand = new Command(() => { /* … */ });
            ChangeStatusCommand = new Command(() => { /* … */ });
            AddUserCommand = new Command(() => { /* … */ });

            BindingContext = this;
        }

        private async void OnDashboardTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//dashboard");
        }

        // ⇩ New handler ⇩
        private async void OnBookOperationsTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//bookoperations");
        }

        private async void OnSearchTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//search");
        }

    }
}
