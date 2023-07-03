using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Employe
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public DateTime? DateNaissance { get; set; }

    public string? Description { get; set; }

    public int ServiceId { get; set; }

    public int FonctionId { get; set; }

    public string? ImageUrl { get; set; }

    public int? LocalisationId { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual Fonction Fonction { get; set; } = null!;

    public virtual Localisation? Localisation { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
