using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BibliothequariaFrontend.Services;
using System.Net.Http;  


namespace BibliothequariaFrontend
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                });
            // ---- API wiring ----
            // If your backend runs on HTTP http://localhost:5195 (see Bibliothequaria.http), use this:
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5195/")  // desktop uses localhost
            });

            builder.Services.AddHttpClient<KnjigaService>(c =>
            {
                c.BaseAddress = new Uri("http://localhost:5195/"); // same place you set MemberService
            });

            //settings
            builder.Services.AddSingleton<BibliothequariaFrontend.Services.ISettingsService,
                              BibliothequariaFrontend.Services.SettingsService>();


            // Our API wrapper
            builder.Services.AddSingleton<MemberService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Allow pages to resolve services via a helper
            ServiceHelper.Services = app.Services;

            return app;
        }
    }

    public static class ServiceHelper
    {
        public static IServiceProvider Services { get; set; } = default!;
        public static T GetRequiredService<T>() where T : notnull
            => Services.GetRequiredService<T>();
    }
}
