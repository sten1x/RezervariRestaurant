using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models.DBObjects;

public partial class MenuItem
{
    [Key]
    public Guid IdMenuItem { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
