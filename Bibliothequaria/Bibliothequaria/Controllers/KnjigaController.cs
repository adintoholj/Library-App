using Bibliothequaria.Models;
using Bibliothequaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bibliothequaria.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KnjigaController : ControllerBase
    {
        BibliothequariaContext db = new BibliothequariaContext();

        [HttpGet]
        public async Task<IActionResult> ListaSvihKnjiga() //za select svega
        {
            List<Knjiga> podaci = db.Knjigas.OrderByDescending(r => r.Id).ToList();
            return Ok(podaci);
        }

        [HttpGet]
        public async Task<IActionResult> PrikaziKnjigeNaslov(string parametar) //za select po naslovu, jedna knjiga
        {
            Knjiga rezultat = db.Knjigas.Where(r => r.Zanr.Contains(parametar)).FirstOrDefault();
            //select * from  where....
            return Ok(rezultat);
        }

        [HttpGet]
        public async Task<IActionResult> PrikaziKnjigeAutor(string parametar) //za select po autoru
        {
            List<Knjiga> rezultat = db.Knjigas.Where(r => r.Autor.Contains(parametar)).ToList();
            //select * from  where....
            return Ok(rezultat);
        }

        [HttpGet]
        public async Task<IActionResult> PrikaziKnjigeZanr(string parametar) //za select po zanru
        {
            List<Knjiga> rezultat = db.Knjigas.Where(r => r.Zanr.Contains(parametar)).ToList();
            //select * from  where....
            return Ok(rezultat);
        }

        [HttpPost]
        public async Task<IActionResult> unesi([FromBody] KnjigaCreateDTO dto)     //za unos novog podatka
        {
            var knjiga = new Knjiga
            {
                Naslov = dto.Naslov,
                Autor = dto.Autor,
                Zanr = dto.Zanr,
                BrojStrana = dto.BrojStrana
            };

            db.Add(knjiga);
            await db.SaveChangesAsync();
            return Ok(knjiga);
        }

        [HttpDelete("{parametar:int}")]
        public IActionResult Obrisi(int parametar)  //za brisanje po ID
        {
            Knjiga rezultat = db.Knjigas.Where(r => r.Id == parametar).FirstOrDefault();
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

        //update

        [HttpPost]

        public async Task<IActionResult> Izmijeni([FromBody] KnjigaUpdateDTO podaci) //za izmjenu
        {
            Knjiga rezultat = db.Knjigas.Where(r => r.Id == podaci.Id).FirstOrDefault();
            //select * from  where....
            if (rezultat == null)
            { return NotFound($"Podatak sa Id = {podaci.Id} nije pronadjen"); }
            else
            {
                rezultat.Naslov = podaci.Naslov;
                rezultat.Autor = podaci.Autor;
                rezultat.Zanr = podaci.Zanr;
                rezultat.BrojStrana = podaci.BrojStrana;
                db.Update(rezultat);
                await db.SaveChangesAsync();
            }
            //logika je da ukucas id radnika kojem hoces da izmijenis podatke i tako mijenjas
            return Ok(rezultat);
        }
    }
}
