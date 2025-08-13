using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace BibliothequariaFrontend.Services
{
    public interface ISettingsService
    {
        bool DarkMode { get; set; }
        bool RequirePasswordOnStartup { get; set; }
        bool Notifications { get; set; }
        string ThemeAccent { get; set; } // "Amber", "Teal", "Purple"
        void Initialize();
    }

    public class SettingsService : ISettingsService
    {
        const string D = "DarkMode", R = "RequirePw", N = "Notifications", A = "ThemeAccent";

        public bool DarkMode
        {
            get => Preferences.Default.Get(D, false);
            set { Preferences.Default.Set(D, value); ApplyTheme(value); }
        }
        public bool RequirePasswordOnStartup
        {
            get => Preferences.Default.Get(R, false);
            set => Preferences.Default.Set(R, value);
        }
        public bool Notifications
        {
            get => Preferences.Default.Get(N, false);
            set => Preferences.Default.Set(N, value);
        }
        public string ThemeAccent
        {
            get => Preferences.Default.Get(A, "Amber");
            set { Preferences.Default.Set(A, value); ApplyAccent(value); }
        }

        public void Initialize()
        {
            ApplyTheme(DarkMode);
            ApplyAccent(ThemeAccent);
        }

        static void ApplyTheme(bool dark)
        {
            Application.Current.UserAppTheme = dark ? AppTheme.Dark : AppTheme.Light;

            var res = Application.Current.Resources;

            if (dark)
            {
                res["PageBackground"] = Color.FromArgb("#2B2B2B");
                res["SurfaceBackground"] = Color.FromArgb("#363636");
                res["PrimaryText"] = Color.FromArgb("#F2F2F2");
            }
            else
            {
                res["PageBackground"] = Color.FromArgb("#FAFAFA");
                res["SurfaceBackground"] = Color.FromArgb("#FFFFFF");
                res["PrimaryText"] = Color.FromArgb("#1A1A1A");
            }
        }

        static void ApplyAccent(string accentKey)
        {
            // (Optional) remove any accent dictionary we merged in App.xaml
            var md = Application.Current.Resources.MergedDictionaries;
            var existingAccent = md.FirstOrDefault(d =>
                d.Source?.OriginalString.Contains("Resources/Styles/Accents/Accent.", StringComparison.OrdinalIgnoreCase) == true);
            if (existingAccent != null)
                md.Remove(existingAccent);

            // Pick the color
            var color = accentKey switch
            {
                "Teal" => Color.FromArgb("#009688"),
                "Purple" => Color.FromArgb("#7E57C2"),
                "Default" or "System" => null, // fall back to base Colors.xaml if you want
                _ => Color.FromArgb("#FFC107") // Amber
            };

            // Apply at the root so all {DynamicResource AccentColor} update live
            if (color is null)
            {
                // If you want "Default" to REALLY remove the override:
                if (Application.Current.Resources.ContainsKey("AccentColor"))
                    Application.Current.Resources.Remove("AccentColor");
                return;
            }

            if (Application.Current.Resources.ContainsKey("AccentColor"))
                Application.Current.Resources["AccentColor"] = color;
            else
                Application.Current.Resources.Add("AccentColor", color);
        }



    }
}