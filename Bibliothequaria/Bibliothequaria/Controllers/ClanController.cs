using Bibliothequaria.Models;
using Bibliothequaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bibliothequaria.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClanController : ControllerBase
    {
        BibliothequariaContext db = new BibliothequariaContext();

        //get svih clanova
        [HttpGet]

        public async Task<IActionResult> PrikaziSveClanove()
        {
            List<Clan> podaci = await db.Clans.OrderByDescending(c => c.Id).ToListAsync();
            return Ok(podaci);
        }


        [HttpPost]

        //za unos novog podatka
        public async Task<IActionResult> unesi([FromBody] ClanCreateDTOcs dto)
        {

            var clan = new Clan
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                DatumUclane = dto.DatumUclane
            };
            db.Add(clan);
            await db.SaveChangesAsync();
            return Ok(clan);
        }

        //delete ZA TESTING PURPOSES, NE ZA KORISNIKA!

        [HttpDelete("{parametar:int}")]
        public IActionResult Obrisi(int parametar)  //za brisanje po ID
        {
            Clan rezultat = db.Clans.Where(r => r.Id == parametar).FirstOrDefault();
            //select * from  where....
            if (rezultat == null)
            { return NotFound($"Podatak sa Id = {parametar} nije pronadjen"); }
            else
            {
                db.Remove(rezultat);
                db.SaveChanges();
            }
            return Ok(parametar);
        }

        [HttpPost]

        public async Task<IActionResult> Izmijeni([FromBody] ClanUpdateDTO dto)
        {
            Clan rezultat = db.Clans.Where(c => c.Id == dto.Id).FirstOrDefault();
            if (rezultat == null) { return NotFound($"Podatak sa ID {dto.Id} nije pronadjen."); }
            else
            {
                rezultat.Ime = dto.Ime;
                rezultat.Prezime = dto.Prezime;
                rezultat.DatumUclane = dto.DatumUclane;
                db.Update(rezultat);
                await db.SaveChangesAsync();
            }
            return Ok(rezultat);
        }
    }
}
