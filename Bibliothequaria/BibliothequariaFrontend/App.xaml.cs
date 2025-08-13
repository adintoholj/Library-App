// App.xaml.cs
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace BibliothequariaFrontend
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var settings = ServiceHelper.GetRequiredService<BibliothequariaFrontend.Services.ISettingsService>();
            settings.Initialize();

            bool dark = Preferences.Default.Get("DarkMode", false);
            var res = Application.Current.Resources;
            res["ContentBackground"] = dark ? Color.FromArgb("#3B3838") : Colors.White;
            res["ContentTextColor"] = dark ? Colors.White : Color.FromArgb("#1A1A1A");



            // 1) Catch non-UI (AppDomain) exceptions
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    Debug.WriteLine($"[UNHANDLED] {ex}");
            };

            // 2) Catch unobserved Task exceptions
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Debug.WriteLine($"[TASK EX] {e.Exception}");
                e.SetObserved();
            };

            MainPage = new AppShell();
        }
    }
}
