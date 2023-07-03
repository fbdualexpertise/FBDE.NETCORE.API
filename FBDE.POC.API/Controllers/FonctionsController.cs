using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FBDE.POC.API.DAL.Model;
using FBDE.POC.API.DAL.ORM.EFCore;

namespace FBDE.POC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FonctionsController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public FonctionsController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/Fonctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fonction>>> GetFonctions()
        {
          if (_context.Fonctions == null)
          {
              return NotFound();
          }
            return await _context.Fonctions.ToListAsync();
        }

        // GET: api/Fonctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fonction>> GetFonction(int id)
        {
          if (_context.Fonctions == null)
          {
              return NotFound();
          }
            var fonction = await _context.Fonctions.FindAsync(id);

            if (fonction == null)
            {
                return NotFound();
            }

            return fonction;
        }

        // PUT: api/Fonctions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFonction(int id, Fonction fonction)
        {
            if (id != fonction.Id)
            {
                return BadRequest();
            }

            _context.Entry(fonction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FonctionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fonctions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fonction>> PostFonction(Fonction fonction)
        {
          if (_context.Fonctions == null)
          {
              return Problem("Entity set 'FbdePocDbContext.Fonctions'  is null.");
          }
            _context.Fonctions.Add(fonction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FonctionExists(fonction.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFonction", new { id = fonction.Id }, fonction);
        }

        // DELETE: api/Fonctions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFonction(int id)
        {
            if (_context.Fonctions == null)
            {
                return NotFound();
            }
            var fonction = await _context.Fonctions.FindAsync(id);
            if (fonction == null)
            {
                return NotFound();
            }

            _context.Fonctions.Remove(fonction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FonctionExists(int id)
        {
            return (_context.Fonctions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
