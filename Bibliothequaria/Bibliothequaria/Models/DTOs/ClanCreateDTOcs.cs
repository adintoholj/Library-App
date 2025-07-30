using System.ComponentModel.DataAnnotations;

namespace Bibliothequaria.Models.DTOs
{
    public class ClanCreateDTOcs
    {
        [Required]
        public string Ime { get; set; } = null!;

        [Required]
        public string Prezime { get; set; } = null!;

        [Required]
        public DateOnly DatumUclane { get; set; }
    }
}
