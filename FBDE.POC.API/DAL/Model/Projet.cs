using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Projet
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Libelle { get; set; }

    public string? Description { get; set; }

    public DateTime? Debut { get; set; }

    public DateTime? Fin { get; set; }

    public decimal? Cout { get; set; }

    public int ServiceId { get; set; }

    public int ClientId { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<FactureProjetClient> FactureProjetClients { get; set; } = new List<FactureProjetClient>();

    public virtual Service Service { get; set; } = null!;
}
