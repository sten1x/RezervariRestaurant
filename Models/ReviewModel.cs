using RezervariRestaurant.Models.DBObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RezervariRestaurant.Models
{
    public class ReviewModel
    {
        [Key]
        public Guid IdReview { get; set; } = Guid.NewGuid();

        [Required]
        public string IdUser { get; set; }


        [Required]
        public string Text { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}
