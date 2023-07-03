using System;
using System.Collections.Generic;

namespace FBDE.POC.API.DAL.Model;

public partial class NotificationPieceJointe
{
    public int Id { get; set; }

    public int? NotificationId { get; set; }

    public int? PieceJointeId { get; set; }

    public virtual Notification? Notification { get; set; }

    public virtual PieceJointe? PieceJointe { get; set; }
}
