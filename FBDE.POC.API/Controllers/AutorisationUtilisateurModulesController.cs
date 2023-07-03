using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FBDE.POC.API.DAL.Model;
using FBDE.POC.API.DAL.ORM.EFCore;
using FBDE.Helpers.Enums;

namespace FBDE.POC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorisationUtilisateurModulesController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public AutorisationUtilisateurModulesController(FbdePocDbContext context)
        {
            _context = context;
        }


        // POST: api/AutorisationUtilisateurModules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<AutorisationUtilisateurModule>> DemanderAutorisationModule(AutorisationUtilisateurModule autorisationUtilisateurModule)
        {
            if (_context.AutorisationUtilisateurModules == null)
            {
                return Problem("Entity set 'FbdePocDbContext.AutorisationUtilisateurModules'  is null.");
            }
            
            try
            {
                autorisationUtilisateurModule.Status = (int)Status_DemandeAutorisationModule.EnAttenteValidation;
                autorisationUtilisateurModule.CreatedBy = autorisationUtilisateurModule.UtilisateurId;
                autorisationUtilisateurModule.CreatedOn = DateTime.Now;

                _context.AutorisationUtilisateurModules.Add(autorisationUtilisateurModule);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AutorisationUtilisateurModuleExists(autorisationUtilisateurModule.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // Garder des traces...

            return CreatedAtAction("GetAutorisationUtilisateurModule", new { id = autorisationUtilisateurModule.Id }, autorisationUtilisateurModule);
        }


        // PUT: api/AutorisationUtilisateurModules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}, {approuve}")]
        public async Task<ActionResult<AutorisationUtilisateurModule>> ValiderDemandeAutorisationModule(int id, bool approuve)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var autorisationUtilisateurModule = await _context.AutorisationUtilisateurModules.FindAsync(id);
            if (autorisationUtilisateurModule == null || autorisationUtilisateurModule.Id <= 0)
            {
                return NotFound();
            }

            _context.Entry(autorisationUtilisateurModule).State = EntityState.Modified;

            try
            {
                autorisationUtilisateurModule.Status = (int)Status_DemandeAutorisationModule.Rejette;
                if (approuve)
                    autorisationUtilisateurModule.Status = (int)Status_DemandeAutorisationModule.Actif;

                //autorisationUtilisateurModule.ModifiedBy = 
                autorisationUtilisateurModule.ModifiedOn = DateTime.Now;

                _context.AutorisationUtilisateurModules.Add(autorisationUtilisateurModule);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorisationUtilisateurModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Garder des traces...

            return autorisationUtilisateurModule;
        }


        // GET: api/AutorisationUtilisateurModules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorisationUtilisateurModule>>> GetAutorisationUtilisateurModules()
        {
          if (_context.AutorisationUtilisateurModules == null)
          {
              return NotFound();
          }
            return await _context.AutorisationUtilisateurModules.ToListAsync();
        }

        // GET: api/AutorisationUtilisateurModules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorisationUtilisateurModule>> GetAutorisationUtilisateurModule(int id)
        {
          if (_context.AutorisationUtilisateurModules == null)
          {
              return NotFound();
          }
            var autorisationUtilisateurModule = await _context.AutorisationUtilisateurModules.FindAsync(id);

            if (autorisationUtilisateurModule == null)
            {
                return NotFound();
            }

            return autorisationUtilisateurModule;
        }

        // PUT: api/AutorisationUtilisateurModules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutorisationUtilisateurModule(int id, AutorisationUtilisateurModule autorisationUtilisateurModule)
        {
            if (id != autorisationUtilisateurModule.Id)
            {
                return BadRequest();
            }

            _context.Entry(autorisationUtilisateurModule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorisationUtilisateurModuleExists(id))
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

        // POST: api/AutorisationUtilisateurModules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<AutorisationUtilisateurModule>> PostAutorisationUtilisateurModule(AutorisationUtilisateurModule autorisationUtilisateurModule)
        {
          if (_context.AutorisationUtilisateurModules == null)
          {
              return Problem("Entity set 'FbdePocDbContext.AutorisationUtilisateurModules'  is null.");
          }
            _context.AutorisationUtilisateurModules.Add(autorisationUtilisateurModule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AutorisationUtilisateurModuleExists(autorisationUtilisateurModule.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAutorisationUtilisateurModule", new { id = autorisationUtilisateurModule.Id }, autorisationUtilisateurModule);
        }

        // DELETE: api/AutorisationUtilisateurModules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutorisationUtilisateurModule(int id)
        {
            if (_context.AutorisationUtilisateurModules == null)
            {
                return NotFound();
            }
            var autorisationUtilisateurModule = await _context.AutorisationUtilisateurModules.FindAsync(id);
            if (autorisationUtilisateurModule == null)
            {
                return NotFound();
            }

            _context.AutorisationUtilisateurModules.Remove(autorisationUtilisateurModule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutorisationUtilisateurModuleExists(int id)
        {
            return (_context.AutorisationUtilisateurModules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
