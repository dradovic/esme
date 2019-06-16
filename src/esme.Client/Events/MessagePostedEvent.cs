namespace esme.Client.Events
{
    public class MessagePostedEvent
    {
        public MessagePostedEvent(int circleId)
        {
            CircleId = circleId;
        }

        public int CircleId { get; }
    }
}
