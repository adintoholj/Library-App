using System.ComponentModel.DataAnnotations;

namespace Bibliothequaria.Models.DTOs
{
    public class RadnikUpdateDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Ime { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Prezime { get; set; } = null!;
        [Required]
        [RegularExpression(@"^\+3876\d{7,8}$",
            ErrorMessage = "Telefon mora biti u formatu +3876xxxxxxx ili +3876xxxxxxxx")]
        public string Telefon { get; set; } = null!;
        [Required, EmailAddress, MaxLength(100)]
        public string EMail { get; set; } = null!;
    }
}
