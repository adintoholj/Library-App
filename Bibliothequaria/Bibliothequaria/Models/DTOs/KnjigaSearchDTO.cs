namespace Bibliothequaria.Models.DTOs
{
    public class KnjigaSearchDTO
    {
        public int Id { get; set; }
        public string Naslov { get; set; } = "";
        public string Autor { get; set; } = "";
        public string Zanr { get; set; } = "";
        public bool? Slobodna { get; set; }
    }
}
