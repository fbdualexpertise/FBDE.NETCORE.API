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
    public class FactureProjetClientsController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public FactureProjetClientsController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/FactureProjetClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FactureProjetClient>>> GetFactureProjetClients()
        {
          if (_context.FactureProjetClients == null)
          {
              return NotFound();
          }
            return await _context.FactureProjetClients.ToListAsync();
        }

        // GET: api/FactureProjetClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FactureProjetClient>> GetFactureProjetClient(int id)
        {
          if (_context.FactureProjetClients == null)
          {
              return NotFound();
          }
            var factureProjetClient = await _context.FactureProjetClients.FindAsync(id);

            if (factureProjetClient == null)
            {
                return NotFound();
            }

            return factureProjetClient;
        }

        // PUT: api/FactureProjetClients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactureProjetClient(int id, FactureProjetClient factureProjetClient)
        {
            if (id != factureProjetClient.Id)
            {
                return BadRequest();
            }

            _context.Entry(factureProjetClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactureProjetClientExists(id))
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

        // POST: api/FactureProjetClients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FactureProjetClient>> PostFactureProjetClient(FactureProjetClient factureProjetClient)
        {
          if (_context.FactureProjetClients == null)
          {
              return Problem("Entity set 'FbdePocDbContext.FactureProjetClients'  is null.");
          }
            _context.FactureProjetClients.Add(factureProjetClient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FactureProjetClientExists(factureProjetClient.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFactureProjetClient", new { id = factureProjetClient.Id }, factureProjetClient);
        }

        // DELETE: api/FactureProjetClients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactureProjetClient(int id)
        {
            if (_context.FactureProjetClients == null)
            {
                return NotFound();
            }
            var factureProjetClient = await _context.FactureProjetClients.FindAsync(id);
            if (factureProjetClient == null)
            {
                return NotFound();
            }

            _context.FactureProjetClients.Remove(factureProjetClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FactureProjetClientExists(int id)
        {
            return (_context.FactureProjetClients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
