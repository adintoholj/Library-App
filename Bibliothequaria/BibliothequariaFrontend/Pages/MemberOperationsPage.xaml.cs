using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace BibliothequariaFrontend.Pages
{
    public partial class MemberOperationsPage : ContentPage
    {
        // Your existing button commands
        public ICommand SeeInChargeCommand { get; }
        public ICommand ChangeStatusCommand { get; }
        public ICommand AddUserCommand { get; }

        public MemberOperationsPage()
        {
            InitializeComponent();

            // stub commands—wire these up later
            SeeInChargeCommand = new Command(() => { /* ... */ });
            ChangeStatusCommand = new Command(() => { /* ... */ });
            AddUserCommand = new Command(() => { /* ... */ });

            BindingContext = this;
        }

        //  ⇩⇩ Add this method ⇩⇩
        private async void OnDashboardTapped(object sender, EventArgs e)
        {
            // navigate back to your Dashboard shell route
            await Shell.Current.GoToAsync("//dashboard");
        }
    }
}
