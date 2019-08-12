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
        public bool SentByMe { get; set; }
        public string SenderName { get; set; }

        public void Merge(MessageViewModel storedMessage)
        {
            if (Id != storedMessage.Id) return; // FIXME: da, log warning (should never be the case)

            // note: it is important not to merge the content in the case of voice messages because
            // the posted voice message content has the URL to the local blob in the browser which prevents
            // that the user would have to download the message again
            SentAt = storedMessage.SentAt;
            SenderName = storedMessage.SenderName;
        }

        public bool IsBeingSent => SenderName == null;
    }
}
