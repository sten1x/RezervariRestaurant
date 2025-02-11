using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models.DBObjects;

public partial class Review
{
    [Key]
    public Guid IdReview { get; set; }

    public string IdUser { get; set; }

    public string Text { get; set; } = null!;

    public int Rating { get; set; }
}
