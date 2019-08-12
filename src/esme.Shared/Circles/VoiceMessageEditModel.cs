using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Circles
{
    public class VoiceMessageEditModel
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // setter used by JSON deserializer

        [Required]
        public byte[] Recording { get; set; }
    }
}
