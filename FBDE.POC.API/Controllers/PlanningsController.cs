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
    public class PlanningsController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public PlanningsController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/Plannings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planning>>> GetPlannings()
        {
          if (_context.Plannings == null)
          {
              return NotFound();
          }
            return await _context.Plannings.ToListAsync();
        }

        // GET: api/Plannings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Planning>> GetPlanning(int id)
        {
          if (_context.Plannings == null)
          {
              return NotFound();
          }
            var planning = await _context.Plannings.FindAsync(id);

            if (planning == null)
            {
                return NotFound();
            }

            return planning;
        }

        // PUT: api/Plannings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanning(int id, Planning planning)
        {
            if (id != planning.Id)
            {
                return BadRequest();
            }

            _context.Entry(planning).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanningExists(id))
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

        // POST: api/Plannings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Planning>> PostPlanning(Planning planning)
        {
          if (_context.Plannings == null)
          {
              return Problem("Entity set 'FbdePocDbContext.Plannings'  is null.");
          }
            _context.Plannings.Add(planning);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlanningExists(planning.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlanning", new { id = planning.Id }, planning);
        }

        // DELETE: api/Plannings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanning(int id)
        {
            if (_context.Plannings == null)
            {
                return NotFound();
            }
            var planning = await _context.Plannings.FindAsync(id);
            if (planning == null)
            {
                return NotFound();
            }

            _context.Plannings.Remove(planning);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanningExists(int id)
        {
            return (_context.Plannings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
