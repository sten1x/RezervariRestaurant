using RezervariRestaurant.Models.DBObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models
{
    public class OrderModel
    {
        [Key]
        public Guid IdOrder { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IdReservation { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
