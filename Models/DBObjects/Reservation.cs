using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models.DBObjects;

public partial class Reservation
{
    [Key]
    public Guid IdReservation { get; set; }

    public string IdUser { get; set; }

    public DateTime ReservationDate { get; set; }

    public int Guests { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
