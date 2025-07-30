using System;
using System.Collections.Generic;

namespace Bibliothequaria.Models;

public partial class Radnik
{
    public int Id { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string EMail { get; set; } = null!;

    public virtual ICollection<Transakcija> Transakcijas { get; set; } = new List<Transakcija>();
}
