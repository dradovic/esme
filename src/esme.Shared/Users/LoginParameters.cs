using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Users
{
    public class LoginParameters
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
