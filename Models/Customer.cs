using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies_Rental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsSubscribed { get; set; }

        public MembershipType MembershipType { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}