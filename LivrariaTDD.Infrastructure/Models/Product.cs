using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LivrariaTDD.Infrastructure.Enums;

namespace LivrariaTDD.Infrastructure.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Publishing { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Categories Category { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal PriceOld { get; set; }

        public bool Sale { get; set; }

        public bool Offer { get; set; }

        public string Photo { get; set; }

        [Required]
        public ProductStatus Status { get; set; }
    
        public virtual List<Order> Orders { get; set; }
    }
}