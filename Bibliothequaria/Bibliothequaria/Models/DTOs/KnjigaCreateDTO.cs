namespace Bibliothequaria.Models.DTOs
{
    public class KnjigaCreateDTO
    {
        public string Naslov { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public string Zanr { get; set; } = null!;

        public int BrojStrana { get; set; }
    }
}
