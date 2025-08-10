using System.Net.Http.Json;
using BibliothequariaFrontend.Models;

namespace BibliothequariaFrontend.Services;

public sealed class MemberService
{
    private readonly HttpClient _http;

    // 👇 Add this line
    public Uri? BaseAddress => _http.BaseAddress;

    public MemberService(HttpClient http) => _http = http;

    public async Task<ClanDTO> CreateAsync(ClanCreateDTO dto, CancellationToken ct = default)
    {
        var res = await _http.PostAsJsonAsync("api/Clan/unesi", dto, ct);

        if (!res.IsSuccessStatusCode)
        {
            var body = await res.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException(
                $"POST /api/Clan/unesi failed: {(int)res.StatusCode} {res.ReasonPhrase}\n{body}");
        }

        var created = await res.Content.ReadFromJsonAsync<ClanDTO>(cancellationToken: ct);
        return created ?? throw new InvalidOperationException("Empty response from server.");
    }

    public async Task<List<ClanOverviewDTO>> GetOverviewAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<List<ClanOverviewDTO>>("api/Clan/Pregled/pregled", cancellationToken: ct)
               ?? new List<ClanOverviewDTO>();
    }
}
