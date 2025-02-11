using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models.DBObjects;

public partial class Order
{
    [Key]
    public Guid IdOrder { get; set; }

    public Guid IdReservation { get; set; }

    public virtual Reservation IdReservationNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
