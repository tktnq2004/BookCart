using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCart.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "money")]

        public decimal? Price { get; set; }
        [Column(TypeName = "money")]

        public decimal? PriceDiscount { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public bool? Features { get; set; }
    }
}