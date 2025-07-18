﻿using Bibliothequaria.Models;
using Microsoft.AspNetCore.Mvc;
using Bibliothequaria.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bibliothequaria.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RadnikController : ControllerBase
    {
        BibliothequariaContext db = new BibliothequariaContext();

        [HttpGet]
        public IActionResult PrikaziSveRadnike() //za select svega
        {
            List<Radnik> podaci = db.Radniks.OrderByDescending(r => r.Id).ToList();
            return Ok(podaci);
        }

        [HttpGet]
        public IActionResult PrikaziOsobeFilter(string parametar) //za select po filteru
        {
            List<Radnik> rezultat = db.Radniks.Where(r => r.Ime.Contains(parametar)).ToList();
            //select * from  where....
            return Ok(rezultat);
        }

        [HttpPost]
        public async Task<IActionResult> unesi([FromBody] RadnikCreateDTO podaci)     //za unos novog podatka
        {
            var radnik = new Radnik
            {
                Ime = podaci.Ime,
                Prezime = podaci.Prezime,
                Telefon = podaci.Telefon,
                EMail = podaci.EMail
            };

            db.Add(radnik);
            await db.SaveChangesAsync();
            return Ok(radnik);
        }

        [HttpDelete("{parametar:int}")]
        public IActionResult Obrisi(int parametar)  //za brisanje po ID
        {
            Radnik rezultat = db.Radniks.Where(r => r.Id == parametar).FirstOrDefault();
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


    }
}
