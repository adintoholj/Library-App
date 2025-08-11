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

    public async Task UpdateStatusAsync(int id, bool status, CancellationToken ct = default)
    {
        var payload = new { Id = id, Status = status };
        using var res = await _http.PutAsJsonAsync("api/Clan/ChangeStatus/status", payload, ct); // or api/Clan/ChangeStatus if you use [action]
        res.EnsureSuccessStatusCode();
    }

    /*public async Task<List<LoanHistoryItem>> GetLoanHistoryAsync(int memberId, CancellationToken ct = default)
    {
        // Try the REST-ish route first
        using var res1 = await _http.GetAsync($"api/Transakcija/History/{memberId}", ct);
        if (res1.IsSuccessStatusCode)
            return (await res1.Content.ReadFromJsonAsync<List<LoanHistoryItem>>(cancellationToken: ct)) ?? new();

        // Fallback to query-string variant if you wire it that way
        using var res2 = await _http.GetAsync($"api/Transakcija/History?memberId={memberId}", ct);
        res2.EnsureSuccessStatusCode();
        return (await res2.Content.ReadFromJsonAsync<List<LoanHistoryItem>>(cancellationToken: ct)) ?? new();
    }*/

    public async Task<List<LoanHistoryItem>> GetLoanHistoryAsync(int memberId, CancellationToken ct = default)
    {
        using var res = await _http.GetAsync($"api/Transakcija/History/{memberId}", ct);
        res.EnsureSuccessStatusCode();
        return (await res.Content.ReadFromJsonAsync<List<LoanHistoryItem>>(cancellationToken: ct)) ?? new();
    }

}
