using System;
using System.Collections.Generic;

namespace Bibliothequaria.Models;

public partial class VwClanOverview
{
    public int Id { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public DateOnly DatumUclane { get; set; }

    public DateOnly? DatumIsteka { get; set; }

    public int? BorrowedCount { get; set; }
}
