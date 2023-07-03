using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class PieceJointe
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? LienLocal { get; set; }

    public string? LienFtp { get; set; }

    public string? Type { get; set; }

    public int? ProduitId { get; set; }

    public int? FactureId { get; set; }

    public int? ContratTravailId { get; set; }

    public virtual ICollection<NotificationPieceJointe> NotificationPieceJointes { get; set; } = new List<NotificationPieceJointe>();

    public virtual Produit? Produit { get; set; }
}
