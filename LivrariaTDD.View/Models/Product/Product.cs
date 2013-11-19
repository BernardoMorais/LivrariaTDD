using System.ComponentModel.DataAnnotations;
using LivrariaTDD.Infrastructure.Enums;

namespace LivrariaTDD.Models.Product
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
        [Range(1, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        public Categories Category { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        [Range(1.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(1.0, double.MaxValue)]
        public decimal PriceOld { get; set; }

        public bool Sale { get; set; }

        public bool Offer { get; set; }

        public string Photo { get; set; }

        [Required]
        public ProductStatus Status { get; set; }
    }
}