using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LivrariaTDD.Attributes;

namespace LivrariaTDD.Models.Account
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [Remote("CheckEmail", "Account", ErrorMessage = "Email já cadastrado!")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
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
    }
}