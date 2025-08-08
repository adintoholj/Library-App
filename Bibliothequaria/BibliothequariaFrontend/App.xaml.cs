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
