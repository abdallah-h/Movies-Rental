using System;
using System.ComponentModel.DataAnnotations;


namespace Movies_Rental.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribed { get; set; }

        public DateTime? Birthdate { get; set; }

        public int MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

    }
}