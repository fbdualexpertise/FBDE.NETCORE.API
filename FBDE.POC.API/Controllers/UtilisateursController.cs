using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FBDE.POC.API.DAL.Model;
using FBDE.POC.API.DAL.ORM.EFCore;
using FBDE.Helpers;
using FBDE.Helpers.Enums;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace FBDE.POC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly FbdePocDbContext _context;

        public UtilisateursController(FbdePocDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs/
        [HttpGet("{login}, {password}")]
        public async Task<ActionResult<Utilisateur>> Login(string login, string password)
        {
            var utilisateur = await _context.Utilisateurs.Where(u => u.Login == login && u.Mdp == password).FirstOrDefaultAsync();

            if (utilisateur == null)
            {
                return NotFound();
            }

            // Garder des traces...

            return utilisateur;
        }


        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> DemanderInscription(Utilisateur utilisateur)
        {
            #region Check Data Validation
            if (_context.Utilisateurs == null)
            {
                return Problem("Entity set 'FbdePocDbContext.Utilisateurs'  is null.");
            }

            if (_context.Notifications == null)
            {
                return Problem("Entity set 'FbdePocDbContext.Notifications'  is null.");
            }

            if (utilisateur == null)
            {
                return Problem("Erreur! le paramètre [Utilisateur] récupéré par l'api [DemanderInscription] est nul. ");
            }

            if (string.IsNullOrEmpty(utilisateur.Login) || string.IsNullOrEmpty(utilisateur.Email) || string.IsNullOrEmpty(utilisateur.Mdp))
            {
                return Problem("Erreur! Les informations suivantes ne doivent pas être vides: Login, Email, Mot de passe.");
            }

            if (utilisateur.ClientId <= 0 && utilisateur.EmployeId <= 0)
            {
                return Problem("Erreur! Veillez préciser à quel employé ou client cet profil utilisateur est rattaché.");
            }
            #endregion

            try
            {
                #region Mise à jour données Utilisateur
                utilisateur.Status = (int)Status_DemandeUtilisateur.Inscription_EnAttenteValidation;
                //utilisateur.CreatedBy = demandeur;
                utilisateur.CreatedOn = DateTime.Now;

                _context.Utilisateurs.Add(utilisateur);
                #endregion

                #region Génération d'une notification à l'administrateur
                var notification = new Notification
                {
                    Code = "INSC", // Générer code notification
                    Titre = $"Demande d'inscription nouvel utilisateur [{utilisateur.Email}]",
                    Corps = $"Login: [{utilisateur.Login}]\n" +
                            $"Email: [{utilisateur.Email}]\n" +
                            $"Contact: [{utilisateur.Telephone}]\n" +
                            $"Nom et prenoms: [{(utilisateur.Employe != null ? utilisateur.Employe.Nom : "")} {(utilisateur.Employe != null ? utilisateur.Employe.Prenom : "")}]\n" +
                            $"Fonction: [{(utilisateur.Employe != null ? (utilisateur.Employe.Fonction != null ? utilisateur.Employe.Fonction.Libelle : "") : "")}]"
                };

                notification.Status = (int)Status_Notification.Actif;
                //utilisateur.CreatedBy = demandeur;
                notification.CreatedOn = DateTime.Now;

                _context.Notifications.Add(notification);
                #endregion

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UtilisateurExists(utilisateur.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // Garder des traces...


            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.Id }, utilisateur);
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/DemandeDesinscription")]
        public async Task<ActionResult<Utilisateur>> DemandeDesinscription(int id)
        {
            string codeNotification = string.Empty;
            string titreNotification = string.Empty;
            string corpsNotification = string.Empty;
            List<Commande> commandesClients = null;

            if (id <= 0)
            {
                return BadRequest();
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null || utilisateur.Id <= 0)
            {
                return NotFound();
            }

            if(utilisateur.Employe == null && utilisateur.Client != null)
            {
                return Problem("Erreur! Impossible de récupérer l'employé ou client associé à cet profil utilisateur.");
            }

            #region Génération d'une notification 
            if (utilisateur.Employe != null) // Il s'agit d'un salarié de la boite
            {
                // Envoyer une notification à l'administrateur et au responsable de cet salarié, en fonction du service concerné
                codeNotification = "DM_DSINS_E";
                titreNotification = $"Demande de desinscription de l'utilisateur [{(utilisateur.Employe != null ? utilisateur.Employe.Nom : "")}] d'email: [{utilisateur.Email}]";
                corpsNotification = $"Login: [{utilisateur.Login}]\n" +
                                    $"Email: [{utilisateur.Email}]\n" +
                                    $"Contact: [{utilisateur.Telephone}]\n" +
                                    $"Nom & prénom(s) : [{(utilisateur.Employe != null ? utilisateur.Employe.Nom : "")} {(utilisateur.Employe != null ? utilisateur.Employe.Prenom : "")}]\n" +
                                    $"Service: [{(utilisateur.Employe != null ? (utilisateur.Employe.Service != null ? utilisateur.Employe.Service.Libelle : "") : "")}]\n" +
                                    $"Fonction: [{(utilisateur.Employe != null ? (utilisateur.Employe.Fonction != null ? utilisateur.Employe.Fonction.Libelle : "") : "")}]";
            }

            if (utilisateur.Client != null)
            {
                codeNotification = "DDM_DSINS_C";
                titreNotification = $"Demande de desinscription du client [{(utilisateur.Employe != null ? utilisateur.Employe.Nom : "")}] d'email: [{utilisateur.Email}]";
                
                commandesClients = _context.Commandes.Where(c => c.ClientId == utilisateur.ClientId && utilisateur.ClientId > 0).ToList();
                if (commandesClients.Any())
                {
                    codeNotification = "T_DSINS_C";
                    titreNotification = $"Tentative de desinscription d'un client ayant au moins une commande en cours. Nom: [{(utilisateur.Employe != null ? utilisateur.Employe.Nom : "")}] Email: [{utilisateur.Email}]";
                }

                corpsNotification = $"Login: [{utilisateur.Login}]\n" +
                                    $"Email: [{utilisateur.Email}]\n" +
                                    $"Contact: [{utilisateur.Telephone}]\n" +
                                    $"Raison sociale : [{(utilisateur.Client != null ? utilisateur.Client.RaisonSociale : "")}]";
            }

            var notification = new Notification
            {
                Code = codeNotification,
                Titre = titreNotification,
                Corps = corpsNotification,
                Status = (int)Status_Notification.Actif,
                //CreatedBy = demandeur;
                CreatedOn = DateTime.Now,
            };

            _context.Notifications.Add(notification);
            #endregion 

            // Dans le cas ou la tentative de desinscription vennait d'un client ayant au moins une commande en cours, stopper le processus et le contraindre à d'abord gérer ces commandes en cours.
            if (commandesClients != null && commandesClients.Any())
                return Problem($"Attention! Vous ne pouvez pas vous desinscrire parce que vous avez {commandesClients.Count()} commande(s) en cours à traiter au préalable.");

            // On poursuit la mise à jour de l'utilisateur (client sans commande en cours ou employé)
            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                utilisateur.Status = (int)Status_DemandeUtilisateur.Desinscription_EnAttenteValidation;

                //utilisateur.ModifiedBy = 
                utilisateur.ModifiedOn = DateTime.Now;

                _context.Utilisateurs.Add(utilisateur);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Garder des traces...

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}, {approuve}")]
        public async Task<ActionResult<Utilisateur>> ValiderDemandeUtilisateur(int id, bool approuve)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null || utilisateur.Id <= 0)
            {
                return NotFound();
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                if (utilisateur.Status == (int)Status_DemandeUtilisateur.Inscription_EnAttenteValidation)
                {
                    utilisateur.Status = (int)Status_DemandeUtilisateur.Inscription_Rejette;
                    if (approuve)
                        utilisateur.Status = (int)Status_DemandeUtilisateur.Actif;
                }
                else if (utilisateur.Status == (int)Status_DemandeUtilisateur.Desinscription_EnAttenteValidation)
                {
                    utilisateur.Status = (int)Status_DemandeUtilisateur.Desinscription_Rejette;
                    if (approuve)
                        utilisateur.Status = (int)Status_DemandeUtilisateur.Suspendu;
                }

                //utilisateur.ModifiedBy = 
                utilisateur.ModifiedOn = DateTime.Now;

                _context.Utilisateurs.Add(utilisateur);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Garder des traces...

            return utilisateur;
        }


        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
          if (_context.Utilisateurs == null)
          {
              return NotFound();
          }
            return await _context.Utilisateurs.ToListAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
          if (_context.Utilisateurs == null)
          {
              return NotFound();
          }
            var utilisateur = await _context.Utilisateurs.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/PutUtilisateur")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
          if (_context.Utilisateurs == null)
          {
              return Problem("Entity set 'FbdePocDbContext.Utilisateurs'  is null.");
          }
            _context.Utilisateurs.Add(utilisateur);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UtilisateurExists(utilisateur.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.Id }, utilisateur);
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return NotFound();
            }
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateurExists(int id)
        {
            return (_context.Utilisateurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
