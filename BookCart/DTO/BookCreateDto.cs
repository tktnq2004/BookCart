using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCart.DTO
{
    public class BookCreateDto
    {
        [Required]
        [StringLength(50)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal Price { get; set; }
        [Column(TypeName = "money")]

        public decimal PriceDiscount { get; set; }
        [Required]
        public IFormFile? Image { get; set; }

    }
}
