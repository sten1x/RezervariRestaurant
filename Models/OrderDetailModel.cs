using RezervariRestaurant.Models.DBObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models
{
    public class OrderDetailModel
    {
        [Key]
        public Guid IdOrderDetail { get; set; } = Guid.NewGuid();

        [Required]
        public Guid IdOrder { get; set; }

        [Required]
        public Guid IdMenuItem { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
