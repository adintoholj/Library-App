using Bibliothequaria.Models;
using Bibliothequaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    // for *Async extensions

namespace Bibliothequaria.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KnjigaController : ControllerBase
    {
        private readonly BibliothequariaContext db = new();

        [HttpGet]
        public async Task<IActionResult> ListaSvihKnjiga()
        {
            // ← await the async ToList
            var podaci = await db.Knjigas
                                 .OrderByDescending(r => r.Id)
                                 .ToListAsync();
            return Ok(podaci);
        }

        [HttpGet]
        public async Task<IActionResult> PrikaziKnjigeNaslov(string parametar)
        {
            // ← await the async FirstOrDefault
            var rezultat = await db.Knjigas
                                   .Where(r => r.Zanr.Contains(parametar))
                                   .FirstOrDefaultAsync();
            return Ok(rezultat);
        }

        [HttpGet]
        public async Task<IActionResult> PrikaziKnjigeAutor(string parametar)
        {
            // ← await the async ToList
            var rezultat = await db.Knjigas
                                   .Where(r => r.Autor.Contains(parametar))
                                   .ToListAsync();
            return Ok(rezultat);
        }

        [HttpGet]
        public async Task<IActionResult> PrikaziKnjigeZanr(string parametar)
        {
            // ← await the async ToList
            var rezultat = await db.Knjigas
                                   .Where(r => r.Zanr.Contains(parametar))
                                   .ToListAsync();
            return Ok(rezultat);
        }

        [HttpPost]
        public async Task<IActionResult> unesi([FromBody] KnjigaCreateDTO dto)
        {
            var knjiga = new Knjiga
            {
                Naslov    = dto.Naslov,
                Autor     = dto.Autor,
                Zanr      = dto.Zanr,
                BrojStrana= dto.BrojStrana
            };

            // ← you can also use AddAsync
            await db.Knjigas.AddAsync(knjiga);
            await db.SaveChangesAsync();           // ← await here
            return Ok(knjiga);
        }

        [HttpDelete("{parametar:int}")]
        public async Task<IActionResult> Obrisi(int parametar)
        {
            // ← await the async find
            var rezultat = await db.Knjigas
                                   .FirstOrDefaultAsync(r => r.Id == parametar);
            if (rezultat == null)
                return NotFound($"Podatak sa Id = {parametar} nije pronadjen");

            db.Knjigas.Remove(rezultat);
            await db.SaveChangesAsync();           // ← await here
            return Ok(parametar);
        }

        [HttpPost]
        public async Task<IActionResult> Izmijeni([FromBody] KnjigaUpdateDTO podaci)
        {
            // ← await the async find
            var rezultat = await db.Knjigas
                                   .FirstOrDefaultAsync(r => r.Id == podaci.Id);
            if (rezultat == null)
                return NotFound($"Podatak sa Id = {podaci.Id} nije pronadjen");

            rezultat.Naslov     = podaci.Naslov;
            rezultat.Autor      = podaci.Autor;
            rezultat.Zanr       = podaci.Zanr;
            rezultat.BrojStrana = podaci.BrojStrana;

            db.Knjigas.Update(rezultat);
            await db.SaveChangesAsync();           // ← await here
            return Ok(rezultat);
        }

        [HttpPut] // Route: api/Knjiga/ChangeStatus
        public async Task<IActionResult> ChangeStatus([FromBody] KnjigaStatusDTO dto)
        {
            if (dto is null) return BadRequest();

            var knjiga = await db.Knjigas.FindAsync(dto.Id);
            if (knjiga is null) return NotFound($"Knjiga Id={dto.Id} nije pronađena.");

            knjiga.Slobodna = dto.Slobodna;   // entity is bool?; assigning bool is fine
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnjigaSearchDTO>>> Search([FromQuery] string q)
        {
            q = (q ?? "").Trim();
            if (q.Length < 2) return Ok(Array.Empty<KnjigaSearchDTO>());

            var terms = q.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                         .Select(t => t.ToLower()).ToArray();

            // AND all terms; each term can match title OR author OR genre
            IQueryable<Knjiga> baseQ = db.Knjigas.AsNoTracking();
            foreach (var t in terms)
            {
                baseQ = baseQ.Where(k =>
                    k.Naslov.ToLower().Contains(t) ||
                    k.Autor.ToLower().Contains(t) ||
                    k.Zanr.ToLower().Contains(t));
            }

            var list = await baseQ
                .OrderBy(k => k.Naslov)
                .Select(k => new KnjigaSearchDTO
                {
                    Id = k.Id,
                    Naslov = k.Naslov,
                    Autor = k.Autor,
                    Zanr = k.Zanr,
                    Slobodna = k.Slobodna
                })
                .ToListAsync();

            return Ok(list);
        }


        //for the book operations screen

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnjigaListDTO>>> Available()
        {
            var list = await db.Knjigas.AsNoTracking()
                .Where(k => k.Slobodna == true)
                .OrderBy(k => k.Naslov)
                .Select(k => new KnjigaListDTO { Id = k.Id, Naslov = k.Naslov, Autor = k.Autor })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnjigaListDTO>>> Borrowed()
        {
            var list = await db.Knjigas.AsNoTracking()
                .Where(k => k.Slobodna == false)
                .OrderBy(k => k.Naslov)
                .Select(k => new KnjigaListDTO { Id = k.Id, Naslov = k.Naslov, Autor = k.Autor })
                .ToListAsync();

            return Ok(list);
        }
    }
}
