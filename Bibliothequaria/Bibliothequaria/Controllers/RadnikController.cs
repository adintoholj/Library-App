using Bibliothequaria.Models;
using Bibliothequaria.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;               // <-- enables FirstOrDefaultAsync, FindAsync, etc.
using Bibliothequaria.Services;          




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

        //new worker

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

        //delete ZA TESTING PURPOSES, NE ZA KORISNIKA!

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

        //update

        [HttpPost]

        public async Task<IActionResult> Izmijeni([FromBody] RadnikUpdateDTO podaci) //za izmjenu
        {
            Radnik rezultat = db.Radniks.Where(r => r.Id == podaci.Id).FirstOrDefault();
            //select * from  where....
            if (rezultat == null)
            { return NotFound($"Podatak sa Id = {podaci.Id} nije pronadjen"); }
            else
            {
                rezultat.Ime = podaci.Ime;
                rezultat.Prezime = podaci.Prezime;
                rezultat.Telefon = podaci.Telefon;
                rezultat.EMail = podaci.EMail;
                db.Update(rezultat);
                await db.SaveChangesAsync();
            }
            //logika je da ukucas id radnika kojem hoces da izmijenis podatke i tako mijenjas
            return Ok(rezultat);
        }

        // ===== REGISTER (START) =====
        [HttpPost("register")]
        public async Task<ActionResult<RadnikAuthResponseDTO>> Register([FromBody] RegisterRadnikDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EMail) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            var email = dto.EMail.Trim();

            // entity property is EMail (mapped to [E-mail])
            var existing = await db.Radniks.FirstOrDefaultAsync(r => r.EMail == email);

            if (existing != null && existing.PasswordHash != null)
                return Conflict("An account with this email already exists.");

            var target = existing ?? new Radnik
            {
                Ime = (dto.Ime ?? "").Trim(),
                Prezime = (dto.Prezime ?? "").Trim(),
                Telefon = dto.Telefon,
                EMail = email           // <-- EMail
            };

            var (hash, salt, iters) = PasswordCrypto.CreateHash(dto.Password);
            target.PasswordHash = hash;
            target.PasswordSalt = salt;
            target.PasswordHashIterations = iters;

            if (existing == null)
                db.Radniks.Add(target);

            await db.SaveChangesAsync();

            // DTO can expose Email (string) even though entity uses EMail
            return new RadnikAuthResponseDTO
            {
                ID = target.Id,
                Ime = target.Ime,
                Prezime = target.Prezime,
                EMail = target.EMail,   // <-- map back to DTO.Email
                Telefon = target.Telefon
            };
        }
        // ===== REGISTER (END) =====


        // ===== LOGIN (START) =====
        [HttpPost("login")]
        public async Task<ActionResult<RadnikAuthResponseDTO>> Login([FromBody] LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EMail) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            var email = dto.EMail.Trim();
            var user = await db.Radniks.FirstOrDefaultAsync(r => r.EMail == email); // <-- EMail

            if (user == null || user.PasswordHash == null || user.PasswordSalt == null)
                return Unauthorized("Invalid credentials.");

            var ok = PasswordCrypto.Verify(dto.Password, user.PasswordHash, user.PasswordSalt, user.PasswordHashIterations);
            if (!ok) return Unauthorized("Invalid credentials.");

            return new RadnikAuthResponseDTO
            {
                ID = user.Id,
                Ime = user.Ime,
                Prezime = user.Prezime,
                EMail = user.EMail,     // <-- map back to DTO.Email
                Telefon = user.Telefon
            };
        }
        // ===== LOGIN (END) =====


        // ===== LOGIN / REGISTER (START) =====
        [HttpGet("{id}/profile")]
        public async Task<ActionResult<RadnikAuthResponseDTO>> GetProfile(int id)
        {
            var u = await db.Radniks.FindAsync(id);
            if (u == null) return NotFound();

            return new RadnikAuthResponseDTO
            {
                ID = u.Id,                      // if your prop is ID, use ID here
                Ime = u.Ime,
                Prezime = u.Prezime,
                EMail = u.EMail,
                Telefon = u.Telefon
            };
        }

        [HttpPut("{id}/profile")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] ProfileUpdateDTO dto)
        {
            var u = await db.Radniks.FindAsync(id);
            if (u == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.Ime)) u.Ime = dto.Ime.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Prezime)) u.Prezime = dto.Prezime.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Telefon)) u.Telefon = dto.Telefon.Trim();

            await db.SaveChangesAsync();
            return NoContent();
        }
        // ===== LOGIN / REGISTER (END) =====


    }
}
