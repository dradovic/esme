using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Infrastructure.Data
{
    public class Message
    {
        public const int MaximumMessageTextLength = 8192;

        public long Id { get; set; }

        public int CircleId { get; set; }

        [MaxLength(MaximumMessageTextLength)]
        public string Text { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public Guid SentBy { get; set; }

        [Required, MaxLength(256)]
        public string SenderName { get; set; }
    }
}
