using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Users
{
    public class LoginParameters
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
