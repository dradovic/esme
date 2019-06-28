using esme.Shared.Circles;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esme.Infrastructure.Data
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public int CircleId { get; set; }

        public ContentType ContentType { get; set; }

        [MaxLength(TextMessageEditModel.MaximumMessageTextLength)]
        public string Content { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public Guid SentBy { get; set; }

        [Required, MaxLength(256)]
        public string SenderName { get; set; }
    }
}
