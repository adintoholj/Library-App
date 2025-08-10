using Bibliothequaria.Models;        // entities + DbContext namespace
using Bibliothequaria.Models.DTOs;   // your DTOs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]/[action]")]
public class TransakcijaController : ControllerBase
{
    private readonly BibliothequariaContext db;   // ← change type here

    public TransakcijaController(BibliothequariaContext db)  // ← and here
        => this.db = db;

    [HttpPost]
    public async Task<ActionResult<KnjigaBorrowResultDTO>> Borrow([FromBody] KnjigaBorrowDTO dto)
    {
        var knjiga = await db.Knjigas.FindAsync(dto.IdKnjige);
        if (knjiga is null) return NotFound("Knjiga nije pronađena.");
        if (knjiga.Slobodna == false) return BadRequest("Knjiga nije slobodna.");

        var today = DateOnly.FromDateTime(DateTime.Today);

        var tx = new Transakcija
        {
            Idknjige = dto.IdKnjige,
            Idclana = dto.IdClana,
            Iduposlenika = dto.IdUposlenika,
            DatumPosudbe = today,
            DatumVracanja = null  
        };

        db.Transakcijas.Add(tx);
        knjiga.Slobodna = false;
        await db.SaveChangesAsync();

        return Ok(new KnjigaBorrowResultDTO
        {
            IdTransakcije = tx.Id,
            RokVracanja = today.AddMonths(2)
        });
    }

    [HttpPut]
    public async Task<IActionResult> Return([FromBody] KnjigaReturnDTO dto)
    {
        var tx = await db.Transakcijas
            .Where(t => t.Idknjige == dto.IdKnjige && t.Idclana == dto.IdClana && t.DatumVracanja == null)
            .OrderByDescending(t => t.Id)
            .FirstOrDefaultAsync();

        if (tx is null) return NotFound("Nema aktivne posudbe za ovu knjigu/člana.");

        tx.DatumVracanja = DateOnly.FromDateTime(DateTime.Today);

        var knjiga = await db.Knjigas.FindAsync(dto.IdKnjige);
        if (knjiga is not null) knjiga.Slobodna = true;

        await db.SaveChangesAsync();
        return NoContent();
    }
}
