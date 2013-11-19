using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaTDD.Infrastructure.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public decimal OrderValue { get; set; }
        public decimal FreightValue { get; set; }
        public decimal TotalValue { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public virtual PaymentType PaymentType { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
