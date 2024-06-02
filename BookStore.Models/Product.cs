using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BookStore.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "(*) Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "(*) Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "(*) Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "(*) ISBN is required")]
        public string ISBN { get; set; }

        public string Publisher { get; set; }

        [Required(ErrorMessage = "(*) Price is required")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "(*) Price must be a positive decimal")]
        public double Price { get; set; }

        [DisplayName("In-Stock")]
        [Required(ErrorMessage = "(*) In-Stock is required")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "(*) Stock must be a positive integer")]
        public int Stock { get; set; }

        [ValidateNever]
        public string FrontCover { get; set; }

        [ValidateNever]
        public string BackCover { get; set; }

        [DisplayName("Genre")]
        [Required(ErrorMessage = "(*) Genre is required")]
        public Guid GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        [ValidateNever]
        public Genre Genre { get; set; }
    }
}
