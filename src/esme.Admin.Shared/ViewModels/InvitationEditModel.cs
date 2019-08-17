using System.ComponentModel.DataAnnotations;

namespace esme.Admin.Shared.ViewModels
{
    public class InvitationEditModel
    {
        [Required]
        [EmailAddress]
        public string To { get; set; }
    }
}
