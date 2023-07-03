using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Planning
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Titre { get; set; }

    public string? Contenu { get; set; }

    public DateTime? Debut { get; set; }

    public DateTime? Fin { get; set; }

    /// <summary>
    /// Projet;Prospection;...
    /// </summary>
    public int? Type { get; set; }

    public string? Invites { get; set; }

    public string? Organisateur { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }
}
