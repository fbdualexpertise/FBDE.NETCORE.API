using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Commande
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Libelle { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// CDI;CDD;Stage;Alternance;Journalier
    /// </summary>
    public int ClientId { get; set; }

    public decimal Montant { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<FactureCommandeClient> FactureCommandeClients { get; set; } = new List<FactureCommandeClient>();

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();
}
