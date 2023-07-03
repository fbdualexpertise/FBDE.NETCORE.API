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
    public class PanierAchatsController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public PanierAchatsController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/PanierAchats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PanierAchat>>> GetPanierAchats()
        {
          if (_context.PanierAchats == null)
          {
              return NotFound();
          }
            return await _context.PanierAchats.ToListAsync();
        }

        // GET: api/PanierAchats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PanierAchat>> GetPanierAchat(int id)
        {
          if (_context.PanierAchats == null)
          {
              return NotFound();
          }
            var panierAchat = await _context.PanierAchats.FindAsync(id);

            if (panierAchat == null)
            {
                return NotFound();
            }

            return panierAchat;
        }

        // PUT: api/PanierAchats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanierAchat(int id, PanierAchat panierAchat)
        {
            if (id != panierAchat.Id)
            {
                return BadRequest();
            }

            _context.Entry(panierAchat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PanierAchatExists(id))
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

        // POST: api/PanierAchats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PanierAchat>> PostPanierAchat(PanierAchat panierAchat)
        {
          if (_context.PanierAchats == null)
          {
              return Problem("Entity set 'FbdePocDbContext.PanierAchats'  is null.");
          }
            _context.PanierAchats.Add(panierAchat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PanierAchatExists(panierAchat.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPanierAchat", new { id = panierAchat.Id }, panierAchat);
        }

        // DELETE: api/PanierAchats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanierAchat(int id)
        {
            if (_context.PanierAchats == null)
            {
                return NotFound();
            }
            var panierAchat = await _context.PanierAchats.FindAsync(id);
            if (panierAchat == null)
            {
                return NotFound();
            }

            _context.PanierAchats.Remove(panierAchat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PanierAchatExists(int id)
        {
            return (_context.PanierAchats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
