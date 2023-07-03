using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Login { get; set; }

    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? Mdp { get; set; }

    public string? Description { get; set; }

    public int? EmployeId { get; set; }

    public int? ClientId { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<AutorisationUtilisateurModule> AutorisationUtilisateurModules { get; set; } = new List<AutorisationUtilisateurModule>();

    public virtual Client? Client { get; set; }

    public virtual Employe? Employe { get; set; }

    public virtual ICollection<PanierAchat> PanierAchats { get; set; } = new List<PanierAchat>();
}
