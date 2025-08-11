namespace Bibliothequaria.Models.DTOs
{
    public class KnjigaLoanHistoryDTO
    {
        public string Naslov { get; set; } = "";
        public DateOnly DatumPosudbe { get; set; }
        public DateOnly RokVracanja { get; set; }      // computed = +2 months
        public DateOnly? DatumVracanja { get; set; }   // null when still borrowed
    }
}
