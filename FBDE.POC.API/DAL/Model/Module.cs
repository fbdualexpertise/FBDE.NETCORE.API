using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Module
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Libelle { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<AutorisationUtilisateurModule> AutorisationUtilisateurModules { get; set; } = new List<AutorisationUtilisateurModule>();
}
