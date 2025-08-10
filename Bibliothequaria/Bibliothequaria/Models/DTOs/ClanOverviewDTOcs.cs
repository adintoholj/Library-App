namespace Bibliothequaria.Models.DTOs
{
    public class ClanOverviewDTOcs
    {
        public int Id { get; set; }
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public DateOnly? DatumUclane { get; set; }   // ← nullable

        public bool Status { get; set; }   // was bool?

        public DateOnly? DatumIsteka { get; set; }   // ← nullable, computed column
        public int BorrowedCount { get; set; }     //active loans
    }
}
