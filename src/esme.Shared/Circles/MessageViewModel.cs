using System;

namespace esme.Shared.Circles
{
    public enum ContentType : byte
    {
        Text = 0,
        Voice = 1,
    }

    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public ContentType ContentType { get; set; }
        public string Content { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public string SenderName { get; set; }
    }
}
