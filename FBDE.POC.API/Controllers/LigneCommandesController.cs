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
    public class LigneCommandesController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public LigneCommandesController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/LigneCommandes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LigneCommande>>> GetLigneCommandes()
        {
          if (_context.LigneCommandes == null)
          {
              return NotFound();
          }
            return await _context.LigneCommandes.ToListAsync();
        }

        // GET: api/LigneCommandes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LigneCommande>> GetLigneCommande(int id)
        {
          if (_context.LigneCommandes == null)
          {
              return NotFound();
          }
            var ligneCommande = await _context.LigneCommandes.FindAsync(id);

            if (ligneCommande == null)
            {
                return NotFound();
            }

            return ligneCommande;
        }

        // PUT: api/LigneCommandes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLigneCommande(int id, LigneCommande ligneCommande)
        {
            if (id != ligneCommande.Id)
            {
                return BadRequest();
            }

            _context.Entry(ligneCommande).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LigneCommandeExists(id))
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

        // POST: api/LigneCommandes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LigneCommande>> PostLigneCommande(LigneCommande ligneCommande)
        {
          if (_context.LigneCommandes == null)
          {
              return Problem("Entity set 'FbdePocDbContext.LigneCommandes'  is null.");
          }
            _context.LigneCommandes.Add(ligneCommande);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LigneCommandeExists(ligneCommande.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLigneCommande", new { id = ligneCommande.Id }, ligneCommande);
        }

        // DELETE: api/LigneCommandes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLigneCommande(int id)
        {
            if (_context.LigneCommandes == null)
            {
                return NotFound();
            }
            var ligneCommande = await _context.LigneCommandes.FindAsync(id);
            if (ligneCommande == null)
            {
                return NotFound();
            }

            _context.LigneCommandes.Remove(ligneCommande);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LigneCommandeExists(int id)
        {
            return (_context.LigneCommandes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
