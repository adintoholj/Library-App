using System;
using System.Collections.Generic;

namespace Bibliothequaria.Models;

public partial class Transakcija
{
    public int Id { get; set; }

    public DateOnly DatumPosudbe { get; set; }

    public DateOnly? DatumVracanja { get; set; }

    public int Idknjige { get; set; }

    public int Idclana { get; set; }

    public int Iduposlenika { get; set; }

    public DateOnly? RokVracanja { get; set; }

    public virtual Clan IdclanaNavigation { get; set; } = null!;

    public virtual Knjiga IdknjigeNavigation { get; set; } = null!;

    public virtual Radnik IduposlenikaNavigation { get; set; } = null!;
}
