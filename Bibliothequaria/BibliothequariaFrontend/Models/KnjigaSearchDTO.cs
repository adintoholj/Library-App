using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequariaFrontend.Models
{
    public class KnjigaSearchDTO
    {
        public int Id { get; set; }
        public string Naslov { get; set; } = "";
        public string Autor { get; set; } = "";
        public string Zanr { get; set; } = "";
        public bool? Slobodna { get; set; }

        public string Availability => Slobodna == true ? "Available" : "Borrowed";
    }
}
