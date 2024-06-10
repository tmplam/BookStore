using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [PrimaryKey(nameof(OrderHeaderId), nameof(ProductId))]
    public class OrderDetail
    {
        [Column(Order = 0)]
        public Guid OrderHeaderId { get; set; }

        [ForeignKey(nameof(OrderHeaderId))]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [Column(Order = 1)]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [ValidateNever]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
