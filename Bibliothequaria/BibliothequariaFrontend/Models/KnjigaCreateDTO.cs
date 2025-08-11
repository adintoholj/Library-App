using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequariaFrontend.Models
{
    public class KnjigaCreateDTO
    {
        public string Naslov { get; set; } = "";
        public string Autor { get; set; } = "";
        public string Zanr { get; set; } = "";
        public int BrojStrana { get; set; }
    }
}
