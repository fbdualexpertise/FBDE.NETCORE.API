using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class LigneCommande
{
    public int Id { get; set; }

    public int CommandeId { get; set; }

    public int ProduitId { get; set; }

    public virtual Commande Commande { get; set; } = null!;

    public virtual Produit Produit { get; set; } = null!;
}
