using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Users
{
    public class SignupParameters
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }
    }
}
