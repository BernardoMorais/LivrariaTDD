using System.Collections.Generic;

namespace LivrariaTDD.Infrastructure.Models
{
    public class PaymentType
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string Icon { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
