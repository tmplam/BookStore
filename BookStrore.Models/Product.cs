using System.ComponentModel.DataAnnotations;

namespace BookStrore.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }

        public string FrontCover { get; set; }
        public string BackCover { get; set; }

        [Required]
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
