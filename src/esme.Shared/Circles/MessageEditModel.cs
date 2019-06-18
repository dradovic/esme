using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Circles
{
    public class MessageEditModel
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        public string Text { get; set; }
    }
}
