using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LivrariaTDD.Infrastructure.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        public string Phone { get; set; }

        public string CellPhone { get; set; }

        [Required]
        public string UserType { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
