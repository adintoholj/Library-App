using System.Net.Http.Json;
using BibliothequariaFrontend.Models;

namespace BibliothequariaFrontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        private const string BaseUrl =
#if ANDROID
            "http://10.0.2.2:5000/api/Radnik";   // Android emulator -> host machine
#else
            "http://localhost:5195/api/Radnik"; // Windows/macOS desktop
#endif

        public AuthService(HttpClient http) => _http = http;

        // ===== REGISTER (START) =====
        public async Task<RadnikAuthResponseDTO?> RegisterAsync(RegisterRadnikDTO dto)
        {
            var res = await _http.PostAsJsonAsync($"{BaseUrl}/Register/register", dto);
            if (!res.IsSuccessStatusCode) return null;

            var user = await res.Content.ReadFromJsonAsync<RadnikAuthResponseDTO>();
            if (user != null) SaveSession(user);
            return user;
        }
        // ===== REGISTER (END) =====

        // ===== LOGIN (START) =====
        public async Task<RadnikAuthResponseDTO?> LoginAsync(string email, string password)
        {
            var url = $"{BaseUrl}/login";
            System.Diagnostics.Debug.WriteLine($"LOGIN URL => {url}");
            

            var res = await _http.PostAsJsonAsync($"{BaseUrl}/Login/login",
                          new LoginDTO { EMail = email, Password = password });
            //if (!res.IsSuccessStatusCode) return null;
            if (!res.IsSuccessStatusCode)
            {
                var body = await res.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"LOGIN HTTP {(int)res.StatusCode} {res.StatusCode}: {body}");
                return null;
            }


            var user = await res.Content.ReadFromJsonAsync<RadnikAuthResponseDTO>();
            if (user != null) SaveSession(user);
            return user;
        }
        // ===== LOGIN (END) =====

        public async Task<RadnikAuthResponseDTO?> GetProfileAsync(int id)
        {
            var url = $"{BaseUrl}/GetProfile/{id}/profile";
            System.Diagnostics.Debug.WriteLine($"PROFILE URL => {url}");
            return await _http.GetFromJsonAsync<RadnikAuthResponseDTO>(url);
        }

        public async Task<bool> UpdateProfileAsync(int id, string? ime, string? prezime, string? telefon)
        {
            var res = await _http.PutAsJsonAsync($"{BaseUrl}/UpdateProfile/{id}/profile",
                       new { Ime = ime, Prezime = prezime, Telefon = telefon });
            if (res.IsSuccessStatusCode)
            {
                // keep local session in sync
                if (!string.IsNullOrWhiteSpace(ime) || !string.IsNullOrWhiteSpace(prezime))
                    Preferences.Default.Set("CurrentUserName", $"{ime} {prezime}".Trim());
                if (!string.IsNullOrWhiteSpace(telefon))
                    Preferences.Default.Set("CurrentUserPhone", telefon);
                return true;
            }
            return false;
        }

        private static void SaveSession(RadnikAuthResponseDTO u)
        {
            Preferences.Default.Set("CurrentUserId", u.ID);
            Preferences.Default.Set("CurrentUserName", $"{u.Ime} {u.Prezime}".Trim());
            Preferences.Default.Set("CurrentUserEmail", u.EMail);
            Preferences.Default.Set("CurrentUserPhone", u.Telefon ?? "");
        }

        public static int? GetUserId()
            => Preferences.Default.ContainsKey("CurrentUserId")
               ? Preferences.Default.Get("CurrentUserId", 0)
               : (int?)null;

        public static void SignOut()
        {
            Preferences.Default.Remove("CurrentUserId");
            Preferences.Default.Remove("CurrentUserName");
            Preferences.Default.Remove("CurrentUserEmail");
            Preferences.Default.Remove("CurrentUserPhone");
        }
    }
}
