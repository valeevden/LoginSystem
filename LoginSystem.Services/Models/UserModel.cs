using System.ComponentModel.DataAnnotations;

namespace LoginSystem.Services.Models
{
    public class UserModel : LoginModel
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? PasswordHash { get; set; }
    }
}
