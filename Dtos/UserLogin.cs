using System.ComponentModel.DataAnnotations;

namespace GTCOAgileCOEPortal.Dtos
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
