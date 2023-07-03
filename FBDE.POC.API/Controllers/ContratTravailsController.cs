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
    public class ContratTravailsController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public ContratTravailsController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/ContratTravails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratTravail>>> GetContratTravails()
        {
          if (_context.ContratTravails == null)
          {
              return NotFound();
          }
            return await _context.ContratTravails.ToListAsync();
        }

        // GET: api/ContratTravails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratTravail>> GetContratTravail(int id)
        {
          if (_context.ContratTravails == null)
          {
              return NotFound();
          }
            var contratTravail = await _context.ContratTravails.FindAsync(id);

            if (contratTravail == null)
            {
                return NotFound();
            }

            return contratTravail;
        }

        // PUT: api/ContratTravails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContratTravail(int id, ContratTravail contratTravail)
        {
            if (id != contratTravail.Id)
            {
                return BadRequest();
            }

            _context.Entry(contratTravail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratTravailExists(id))
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

        // POST: api/ContratTravails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContratTravail>> PostContratTravail(ContratTravail contratTravail)
        {
          if (_context.ContratTravails == null)
          {
              return Problem("Entity set 'FbdePocDbContext.ContratTravails'  is null.");
          }
            _context.ContratTravails.Add(contratTravail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContratTravailExists(contratTravail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContratTravail", new { id = contratTravail.Id }, contratTravail);
        }

        // DELETE: api/ContratTravails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContratTravail(int id)
        {
            if (_context.ContratTravails == null)
            {
                return NotFound();
            }
            var contratTravail = await _context.ContratTravails.FindAsync(id);
            if (contratTravail == null)
            {
                return NotFound();
            }

            _context.ContratTravails.Remove(contratTravail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContratTravailExists(int id)
        {
            return (_context.ContratTravails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
