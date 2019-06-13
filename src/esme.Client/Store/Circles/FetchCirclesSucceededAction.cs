using Blazor.Fluxor;
using esme.Shared.Circles;

namespace esme.Client.Store.Circles
{
    public class FetchCirclesSucceededAction : IAction
    {
        public FetchCirclesSucceededAction(CircleViewModel[] circles)
        {
            Circles = circles;
        }

        public CircleViewModel[] Circles { get; }
    }
}