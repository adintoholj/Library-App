using System;
using System.Collections.Generic;

namespace Bibliothequaria.Models;

public partial class Knjiga
{
    public int Id { get; set; }

    public string Naslov { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Zanr { get; set; } = null!;

    public int BrojStrana { get; set; }

    public bool? Slobodna { get; set; }

    public virtual ICollection<Transakcija> Transakcijas { get; set; } = new List<Transakcija>();
}
