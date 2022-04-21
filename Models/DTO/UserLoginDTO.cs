using System.ComponentModel.DataAnnotations;

namespace mniaAPI.Models
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}