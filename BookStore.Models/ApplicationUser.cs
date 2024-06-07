using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public Guid? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [ValidateNever]
        public Company? Company { get; set; }

        [NotMapped]
        public string? Role { get; set; }
    }
}
