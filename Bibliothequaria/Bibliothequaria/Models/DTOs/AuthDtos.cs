// ===== REGISTER / LOGIN (START) =====
namespace Bibliothequaria.Models.DTOs
{
    public class RegisterRadnikDTO
    {
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public string EMail { get; set; } = "";
        public string Password { get; set; } = "";
        public string? Telefon { get; set; }
    }

    public class LoginDTO
    {
        public string EMail { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RadnikAuthResponseDTO
    {
        public int ID { get; set; }
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public string EMail { get; set; } = "";
        public string? Telefon { get; set; }
    }

    public class ProfileUpdateDTO
    {
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? Telefon { get; set; }
    }
}
// ===== REGISTER / LOGIN (END) =====
