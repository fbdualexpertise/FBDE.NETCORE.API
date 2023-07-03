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
    public class NotificationPieceJointesController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public NotificationPieceJointesController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/NotificationPieceJointes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationPieceJointe>>> GetNotificationPieceJointes()
        {
          if (_context.NotificationPieceJointes == null)
          {
              return NotFound();
          }
            return await _context.NotificationPieceJointes.ToListAsync();
        }

        // GET: api/NotificationPieceJointes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationPieceJointe>> GetNotificationPieceJointe(int id)
        {
          if (_context.NotificationPieceJointes == null)
          {
              return NotFound();
          }
            var notificationPieceJointe = await _context.NotificationPieceJointes.FindAsync(id);

            if (notificationPieceJointe == null)
            {
                return NotFound();
            }

            return notificationPieceJointe;
        }

        // PUT: api/NotificationPieceJointes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificationPieceJointe(int id, NotificationPieceJointe notificationPieceJointe)
        {
            if (id != notificationPieceJointe.Id)
            {
                return BadRequest();
            }

            _context.Entry(notificationPieceJointe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationPieceJointeExists(id))
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

        // POST: api/NotificationPieceJointes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotificationPieceJointe>> PostNotificationPieceJointe(NotificationPieceJointe notificationPieceJointe)
        {
          if (_context.NotificationPieceJointes == null)
          {
              return Problem("Entity set 'FbdePocDbContext.NotificationPieceJointes'  is null.");
          }
            _context.NotificationPieceJointes.Add(notificationPieceJointe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NotificationPieceJointeExists(notificationPieceJointe.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNotificationPieceJointe", new { id = notificationPieceJointe.Id }, notificationPieceJointe);
        }

        // DELETE: api/NotificationPieceJointes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationPieceJointe(int id)
        {
            if (_context.NotificationPieceJointes == null)
            {
                return NotFound();
            }
            var notificationPieceJointe = await _context.NotificationPieceJointes.FindAsync(id);
            if (notificationPieceJointe == null)
            {
                return NotFound();
            }

            _context.NotificationPieceJointes.Remove(notificationPieceJointe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationPieceJointeExists(int id)
        {
            return (_context.NotificationPieceJointes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
