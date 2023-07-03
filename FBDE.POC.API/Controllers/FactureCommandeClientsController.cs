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
    public class FactureCommandeClientsController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public FactureCommandeClientsController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/FactureCommandeClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FactureCommandeClient>>> GetFactureCommandeClients()
        {
          if (_context.FactureCommandeClients == null)
          {
              return NotFound();
          }
            return await _context.FactureCommandeClients.ToListAsync();
        }

        // GET: api/FactureCommandeClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FactureCommandeClient>> GetFactureCommandeClient(int id)
        {
          if (_context.FactureCommandeClients == null)
          {
              return NotFound();
          }
            var factureCommandeClient = await _context.FactureCommandeClients.FindAsync(id);

            if (factureCommandeClient == null)
            {
                return NotFound();
            }

            return factureCommandeClient;
        }

        // PUT: api/FactureCommandeClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactureCommandeClient(int id, FactureCommandeClient factureCommandeClient)
        {
            if (id != factureCommandeClient.Id)
            {
                return BadRequest();
            }

            _context.Entry(factureCommandeClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactureCommandeClientExists(id))
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

        // POST: api/FactureCommandeClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FactureCommandeClient>> PostFactureCommandeClient(FactureCommandeClient factureCommandeClient)
        {
          if (_context.FactureCommandeClients == null)
          {
              return Problem("Entity set 'FbdePocDbContext.FactureCommandeClients'  is null.");
          }
            _context.FactureCommandeClients.Add(factureCommandeClient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FactureCommandeClientExists(factureCommandeClient.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFactureCommandeClient", new { id = factureCommandeClient.Id }, factureCommandeClient);
        }

        // DELETE: api/FactureCommandeClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactureCommandeClient(int id)
        {
            if (_context.FactureCommandeClients == null)
            {
                return NotFound();
            }
            var factureCommandeClient = await _context.FactureCommandeClients.FindAsync(id);
            if (factureCommandeClient == null)
            {
                return NotFound();
            }

            _context.FactureCommandeClients.Remove(factureCommandeClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FactureCommandeClientExists(int id)
        {
            return (_context.FactureCommandeClients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
