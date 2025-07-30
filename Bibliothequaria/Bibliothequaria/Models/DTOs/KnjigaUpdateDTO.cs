namespace Bibliothequaria.Models.DTOs
{
    public class KnjigaUpdateDTO
    {
        public int Id { get; set; }

        public string Naslov { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public string Zanr { get; set; } = null!;

        public int BrojStrana { get; set; }
    }
}
