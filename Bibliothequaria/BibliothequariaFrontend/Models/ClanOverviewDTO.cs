using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ClanOverviewDTO
{
    public int Id { get; set; }
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateOnly? DatumUclane { get; set; }
    public DateOnly? DatumIsteka { get; set; }
    public int BorrowedCount { get; set; }

    // Display helpers
    public string FullName => $"{Ime} {Prezime}".Trim();
    public string DatumUclaneDisplay => DatumUclane.HasValue ? DatumUclane.Value.ToString("yyyy-MM-dd") : "-";
    public string DatumIstekaDisplay => DatumIsteka.HasValue ? DatumIsteka.Value.ToString("yyyy-MM-dd") : "-";
}