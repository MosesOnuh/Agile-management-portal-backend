﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GTCOAgileCOEPortal.Dtos
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name must be at least {2}, and maximum {15} characters")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
