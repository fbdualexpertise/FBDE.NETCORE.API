﻿using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class FactureProjetClient
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Libelle { get; set; }

    public string? Description { get; set; }

    public decimal? Montant { get; set; }

    public int ProjetId { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual Projet Projet { get; set; } = null!;
}
