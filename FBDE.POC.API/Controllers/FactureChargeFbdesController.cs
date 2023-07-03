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
    public class FactureChargeFbdesController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public FactureChargeFbdesController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/FactureChargeFbdes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FactureChargeFbde>>> GetFactureChargeFbdes()
        {
          if (_context.FactureChargeFbdes == null)
          {
              return NotFound();
          }
            return await _context.FactureChargeFbdes.ToListAsync();
        }

        // GET: api/FactureChargeFbdes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FactureChargeFbde>> GetFactureChargeFbde(int id)
        {
          if (_context.FactureChargeFbdes == null)
          {
              return NotFound();
          }
            var factureChargeFbde = await _context.FactureChargeFbdes.FindAsync(id);

            if (factureChargeFbde == null)
            {
                return NotFound();
            }

            return factureChargeFbde;
        }

        // PUT: api/FactureChargeFbdes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactureChargeFbde(int id, FactureChargeFbde factureChargeFbde)
        {
            if (id != factureChargeFbde.Id)
            {
                return BadRequest();
            }

            _context.Entry(factureChargeFbde).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactureChargeFbdeExists(id))
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

        // POST: api/FactureChargeFbdes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FactureChargeFbde>> PostFactureChargeFbde(FactureChargeFbde factureChargeFbde)
        {
          if (_context.FactureChargeFbdes == null)
          {
              return Problem("Entity set 'FbdePocDbContext.FactureChargeFbdes'  is null.");
          }
            _context.FactureChargeFbdes.Add(factureChargeFbde);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FactureChargeFbdeExists(factureChargeFbde.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFactureChargeFbde", new { id = factureChargeFbde.Id }, factureChargeFbde);
        }

        // DELETE: api/FactureChargeFbdes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactureChargeFbde(int id)
        {
            if (_context.FactureChargeFbdes == null)
            {
                return NotFound();
            }
            var factureChargeFbde = await _context.FactureChargeFbdes.FindAsync(id);
            if (factureChargeFbde == null)
            {
                return NotFound();
            }

            _context.FactureChargeFbdes.Remove(factureChargeFbde);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FactureChargeFbdeExists(int id)
        {
            return (_context.FactureChargeFbdes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
