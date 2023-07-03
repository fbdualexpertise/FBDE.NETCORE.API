using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class Notification
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Titre { get; set; }

    public string? Corps { get; set; }

    public string Expediteur { get; set; } = null!;

    public string Destinataires { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<NotificationPieceJointe> NotificationPieceJointes { get; set; } = new List<NotificationPieceJointe>();
}
