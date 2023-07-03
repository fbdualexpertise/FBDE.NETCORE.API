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
    public class PieceJointesController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public PieceJointesController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/PieceJointes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PieceJointe>>> GetPieceJointes()
        {
          if (_context.PieceJointes == null)
          {
              return NotFound();
          }
            return await _context.PieceJointes.ToListAsync();
        }

        // GET: api/PieceJointes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PieceJointe>> GetPieceJointe(int id)
        {
          if (_context.PieceJointes == null)
          {
              return NotFound();
          }
            var pieceJointe = await _context.PieceJointes.FindAsync(id);

            if (pieceJointe == null)
            {
                return NotFound();
            }

            return pieceJointe;
        }

        // PUT: api/PieceJointes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPieceJointe(int id, PieceJointe pieceJointe)
        {
            if (id != pieceJointe.Id)
            {
                return BadRequest();
            }

            _context.Entry(pieceJointe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PieceJointeExists(id))
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

        // POST: api/PieceJointes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PieceJointe>> PostPieceJointe(PieceJointe pieceJointe)
        {
          if (_context.PieceJointes == null)
          {
              return Problem("Entity set 'FbdePocDbContext.PieceJointes'  is null.");
          }
            _context.PieceJointes.Add(pieceJointe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PieceJointeExists(pieceJointe.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPieceJointe", new { id = pieceJointe.Id }, pieceJointe);
        }

        // DELETE: api/PieceJointes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePieceJointe(int id)
        {
            if (_context.PieceJointes == null)
            {
                return NotFound();
            }
            var pieceJointe = await _context.PieceJointes.FindAsync(id);
            if (pieceJointe == null)
            {
                return NotFound();
            }

            _context.PieceJointes.Remove(pieceJointe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PieceJointeExists(int id)
        {
            return (_context.PieceJointes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
