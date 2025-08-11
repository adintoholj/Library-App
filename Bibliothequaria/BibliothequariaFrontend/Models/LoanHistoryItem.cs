namespace BibliothequariaFrontend.Models;

public class LoanHistoryItem
{
    public string Naslov { get; set; } = "";         // book title
    public DateOnly DatumPosudbe { get; set; }
    public DateOnly? RokVracanja { get; set; }       // optional (computed on server)
    public DateOnly? DatumVracanja { get; set; }

    public void EnsureComputedDue()
    {
        if (RokVracanja is null)
            RokVracanja = DatumPosudbe.AddMonths(2);
    }

    public string BorrowedDisplay => DatumPosudbe.ToString("yyyy-MM-dd");
    public string DueDisplay => (RokVracanja ?? DatumPosudbe.AddMonths(2)).ToString("yyyy-MM-dd");
    public string ReturnedDisplay => DatumVracanja.HasValue ? DatumVracanja.Value.ToString("yyyy-MM-dd") : "—";
}
