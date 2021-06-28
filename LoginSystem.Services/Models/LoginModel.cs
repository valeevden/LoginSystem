using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Services.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Login { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
