using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Infrastructure.Data
{
    public class Message
    {
        public long Id { get; set; }

        public int CircleId { get; set; }

        [MaxLength(8192)]
        public string Text { get; set; }

        public DateTimeOffset SentAt { get; set; }

        [Required, MaxLength(256)]
        public string SentBy { get; set; }
    }
}
