﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies_Rental.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1, 25, ErrorMessage ="Number in Stock must be between 1 and 25.")]
        public short NumberInStock { get; set; }

        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }


    }
}