using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequariaFrontend.Models
{
    public class KnjigaListDTO
    {
        public int Id { get; set; }
        public string Naslov { get; set; } = "";
        public string Autor { get; set; } = "";
    }
}
