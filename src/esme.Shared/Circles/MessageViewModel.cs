using System;

namespace esme.Shared.Circles
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public string SenderName { get; set; }
    }
}
