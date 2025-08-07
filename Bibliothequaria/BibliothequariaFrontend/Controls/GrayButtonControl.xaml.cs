using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace BibliothequariaFrontend.Controls
{
    public partial class GrayButtonControl : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(GrayButtonControl),
                default(string));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(GrayButtonControl),
                default(ICommand));

        /// <summary>
        /// The text displayed in the button.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The tap command invoked when the button is tapped.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public GrayButtonControl()
        {
            InitializeComponent();
            // NOTE: Do NOT set BindingContext = this;
            // We want it to inherit the page's BindingContext for the Command.
        }
    }
}
