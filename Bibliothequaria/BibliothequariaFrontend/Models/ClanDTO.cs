using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothequariaFrontend.Models;

public class ClanDTO
{
    public int Id { get; set; }
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateOnly DatumUclane { get; set; }
    public bool? Status { get; set; }
}
