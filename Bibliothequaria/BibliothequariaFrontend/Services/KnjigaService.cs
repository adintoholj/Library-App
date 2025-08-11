using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using BibliothequariaFrontend.Models;

namespace BibliothequariaFrontend.Services;

public sealed class KnjigaService
{
    private readonly HttpClient _http;
    public KnjigaService(HttpClient http) => _http = http;

    public async Task<List<KnjigaSearchDTO>> SearchAsync(string q, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(q)) return new();
        using var res = await _http.GetAsync($"api/Knjiga/Search?q={Uri.EscapeDataString(q)}", ct);
        res.EnsureSuccessStatusCode();
        return (await res.Content.ReadFromJsonAsync<List<KnjigaSearchDTO>>(cancellationToken: ct)) ?? new();
    }

    //book operations

    public async Task<List<KnjigaListDTO>> GetAvailableAsync(CancellationToken ct = default)
    {
        using var res = await _http.GetAsync("api/Knjiga/Available", ct);
        res.EnsureSuccessStatusCode();
        return (await res.Content.ReadFromJsonAsync<List<KnjigaListDTO>>(cancellationToken: ct)) ?? new();
    }

    public async Task<List<KnjigaListDTO>> GetBorrowedAsync(CancellationToken ct = default)
    {
        using var res = await _http.GetAsync("api/Knjiga/Borrowed", ct);
        res.EnsureSuccessStatusCode();
        return (await res.Content.ReadFromJsonAsync<List<KnjigaListDTO>>(cancellationToken: ct)) ?? new();
    }

    public async Task<KnjigaDTO> CreateAsync(KnjigaCreateDTO dto, CancellationToken ct = default)
    {
        var res = await _http.PostAsJsonAsync("api/Knjiga/unesi", dto, ct);
        res.EnsureSuccessStatusCode();
        var created = await res.Content.ReadFromJsonAsync<KnjigaDTO>(cancellationToken: ct);
        return created ?? throw new InvalidOperationException("Empty response from server.");
    }
}
