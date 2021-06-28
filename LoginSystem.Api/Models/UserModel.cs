using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Api.Models
{
    public class UserModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        public string HashFromPassword { get; set; }
    }
}
