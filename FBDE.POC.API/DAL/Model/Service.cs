using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Service
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

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual ICollection<FactureChargeFbde> FactureChargeFbdes { get; set; } = new List<FactureChargeFbde>();

    public virtual ICollection<Projet> Projets { get; set; } = new List<Projet>();
}
