using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Localisation
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Continent { get; set; }

    public string? Pays { get; set; }

    public string? Region { get; set; }

    public string? Departement { get; set; }

    public string? Ville { get; set; }

    public string? CodePostal { get; set; }

    public string? Adresse1 { get; set; }

    public string? Adresse2 { get; set; }

    public string? Adresse3 { get; set; }

    public string? LocalisationGps { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}
