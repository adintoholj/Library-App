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
}
