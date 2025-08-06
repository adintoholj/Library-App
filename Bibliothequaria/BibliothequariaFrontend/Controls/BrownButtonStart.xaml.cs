using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace BibliothequariaFrontend.Controls
{
    public partial class BrownButtonStart : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(BrownButtonStart), default(string));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(BrownButtonStart), default(ICommand));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public BrownButtonStart()
        {
            InitializeComponent();
            // NOTE: Do NOT set BindingContext = this; 
            // We want this control to inherit the page's BindingContext
        }
    }
}
