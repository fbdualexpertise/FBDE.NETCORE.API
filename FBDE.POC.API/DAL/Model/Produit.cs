using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Produit
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Libelle { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? PrixHt { get; set; }

    public decimal? PrixTtc { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

    public virtual ICollection<PieceJointe> PieceJointes { get; set; } = new List<PieceJointe>();
}
