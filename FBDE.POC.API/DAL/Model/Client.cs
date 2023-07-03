using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Client
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? RaisonSociale { get; set; }

    /// <summary>
    /// Particulier;Entreprise
    /// </summary>
    public int? Type { get; set; }

    public DateTime? DateCreation { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int LocalisationId { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual Localisation Localisation { get; set; } = null!;

    public virtual ICollection<Projet> Projets { get; set; } = new List<Projet>();

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
