using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models.DBObjects;

public partial class OrderDetail
{
    [Key]
    public Guid IdOrderDetail { get; set; }

    public Guid IdOrder { get; set; }

    public Guid IdMenuItem { get; set; }

    public int Quantity { get; set; }

    public virtual MenuItem IdMenuItemNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
