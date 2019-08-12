using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Circles
{
    public class TextMessageEditModel
    {
        public const int MaximumMessageTextLength = 8192;

        public Guid Id { get; set; } = Guid.NewGuid(); // setter used by JSON deserializer

        [Required]
        [MaxLength(MaximumMessageTextLength)]
        public string Text { get; set; }
    }
}
