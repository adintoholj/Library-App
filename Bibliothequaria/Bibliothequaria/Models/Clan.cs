using System;
using System.Collections.Generic;

namespace Bibliothequaria.Models;

public partial class Clan
{
    public int Id { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public DateOnly DatumUclane { get; set; }

    public virtual ICollection<Transakcija> Transakcijas { get; set; } = new List<Transakcija>();
}
