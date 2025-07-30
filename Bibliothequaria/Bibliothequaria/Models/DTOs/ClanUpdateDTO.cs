namespace Bibliothequaria.Models.DTOs
{
    public class ClanUpdateDTO
    {
        public int Id { get; set; }

        public string Ime { get; set; } = null!;

        public string Prezime { get; set; } = null!;

        public DateOnly DatumUclane { get; set; }
    }
}
