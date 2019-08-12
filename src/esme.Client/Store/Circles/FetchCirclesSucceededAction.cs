using esme.Shared.Circles;

namespace esme.Client.Store.Circles
{
    public class FetchCirclesSucceededAction
    {
        public FetchCirclesSucceededAction(CircleViewModel[] circles)
        {
            Circles = circles;
        }

        public CircleViewModel[] Circles { get; }
    }
}