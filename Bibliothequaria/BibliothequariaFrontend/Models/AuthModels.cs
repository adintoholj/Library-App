namespace BibliothequariaFrontend.Models
{
    // Returned by LOGIN & REGISTER
    public class RadnikAuthResponseDTO
    {
        public int ID { get; set; }
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public string EMail { get; set; } = "";
        public string? Telefon { get; set; }
    }

    // ===== REGISTER (START) =====
    public class RegisterRadnikDTO
    {
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public string EMail { get; set; } = "";
        public string Password { get; set; } = "";
        public string? Telefon { get; set; }
    }
    // ===== REGISTER (END) =====

    // ===== LOGIN (START) =====
    public class LoginDTO
    {
        public string EMail { get; set; } = "";
        public string Password { get; set; } = "";
    }
    // ===== LOGIN (END) =====
}
