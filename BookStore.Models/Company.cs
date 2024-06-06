using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookStore.Models
{
    public class Company
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Company name")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Phone]
        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
