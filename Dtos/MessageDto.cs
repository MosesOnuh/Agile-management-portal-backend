using System;
using System.ComponentModel.DataAnnotations;

namespace GTCOAgileCOEPortal.Dtos
{
    public class MessageDto
    {
        [Required]
        public string UserId { get; set; }
        public string From { get; set; }  //FirstName of users
        public string Role { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
