﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreWeb.Models
{
	public class Genre
	{
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
