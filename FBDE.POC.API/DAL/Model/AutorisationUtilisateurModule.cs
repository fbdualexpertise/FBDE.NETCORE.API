using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class AutorisationUtilisateurModule
{
    public int Id { get; set; }

    public int UtilisateurId { get; set; }

    public int ModuleId { get; set; }

    public int? Create { get; set; }

    public int? Read { get; set; }

    public int? Update { get; set; }

    public int? Delete { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual Module Module { get; set; } = null!;

    public virtual Utilisateur Utilisateur { get; set; } = null!;
}
