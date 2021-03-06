﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Movies_Rental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsSubscribed { get; set; }

        public DateTime? Birthdate { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        public MembershipType MembershipType { get; set; }
    }
}