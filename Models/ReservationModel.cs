using RezervariRestaurant.Models.DBObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RezervariRestaurant.Models
{
    public class ReservationModel
    {
        [Key]
        public Guid IdReservation { get; set; } = Guid.NewGuid();

        [BindNever]
        public string IdUser { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int Guests { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
