using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Circles
{
    public class MessageEditModel
    {
        [Required]
        public string Text { get; set; }
    }
}
