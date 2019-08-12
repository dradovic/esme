using esme.Shared.Circles;
using System;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageAction
    {
        public PostTextMessageAction(Guid circleId, TextMessageEditModel message)
        {
            CircleId = circleId;
            Message = message;
        }

        public Guid CircleId { get; }
        public TextMessageEditModel Message { get; }
    }
}
