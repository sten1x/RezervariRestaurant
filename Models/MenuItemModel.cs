using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models
{
    public class MenuItemModel
    {
        [Key]
        public Guid IdMenuItem { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
