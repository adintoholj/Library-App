namespace Bibliothequaria.Models.DTOs
{
    public class KnjigaStatusDTO
    {
        public int Id { get; set; }
        public bool Slobodna { get; set; }  // true = available, false = not available
    }
}
