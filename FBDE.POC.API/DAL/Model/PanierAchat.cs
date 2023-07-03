using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class PanierAchat
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Libelle { get; set; }

    public string? Description { get; set; }

    public string ListeProduitsId { get; set; } = null!;

    public int? UtilisateurId { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual Utilisateur? Utilisateur { get; set; }
}
