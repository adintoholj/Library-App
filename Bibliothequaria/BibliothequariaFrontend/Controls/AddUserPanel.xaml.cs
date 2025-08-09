using System.Windows.Input;

namespace BibliothequariaFrontend.Controls;

public partial class AddUserPanel : ContentView
{
    public AddUserPanel()
    {
        InitializeComponent();
    }

    // FirstName
    public static readonly BindableProperty FirstNameProperty =
        BindableProperty.Create(nameof(FirstName), typeof(string), typeof(AddUserPanel), default(string));

    public string FirstName
    {
        get => (string)GetValue(FirstNameProperty);
        set => SetValue(FirstNameProperty, value);
    }

    // LastName
    public static readonly BindableProperty LastNameProperty =
        BindableProperty.Create(nameof(LastName), typeof(string), typeof(AddUserPanel), default(string));

    public string LastName
    {
        get => (string)GetValue(LastNameProperty);
        set => SetValue(LastNameProperty, value);
    }

    // SubmitCommand (receives AddUserPayload)
    public static readonly BindableProperty SubmitCommandProperty =
        BindableProperty.Create(nameof(SubmitCommand), typeof(ICommand), typeof(AddUserPanel), default(ICommand));

    public ICommand? SubmitCommand
    {
        get => (ICommand?)GetValue(SubmitCommandProperty);
        set => SetValue(SubmitCommandProperty, value);
    }

    // CancelCommand
    public static readonly BindableProperty CancelCommandProperty =
        BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(AddUserPanel), default(ICommand));

    public ICommand? CancelCommand
    {
        get => (ICommand?)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    // Events if you prefer event handlers over commands
    public event EventHandler<AddUserPayload>? Submitted;
    public event EventHandler? Canceled;

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        var payload = new AddUserPayload
        {
            FirstName = FirstName?.Trim(),
            LastName = LastName?.Trim()
        };

        if (SubmitCommand?.CanExecute(payload) == true)
            SubmitCommand.Execute(payload);

        Submitted?.Invoke(this, payload);
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        if (CancelCommand?.CanExecute(null) == true)
            CancelCommand.Execute(null);

        Canceled?.Invoke(this, EventArgs.Empty);
    }
}

public sealed class AddUserPayload
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
