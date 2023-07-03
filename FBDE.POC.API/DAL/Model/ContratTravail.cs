using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class ContratTravail
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Libelle { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// CDI;CDD;Stage;Alternance;Journalier
    /// </summary>
    public int? Type { get; set; }

    public DateTime? Debut { get; set; }

    public DateTime? Fin { get; set; }

    public decimal? SalaireBrutMensuel { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<FactureChargeFbde> FactureChargeFbdes { get; set; } = new List<FactureChargeFbde>();
}
