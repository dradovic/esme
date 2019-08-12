using Blazor.Fluxor;
using esme.Shared;
using Force.DeepCloner;

namespace esme.Client.Store.Circles
{
    public class IncrementUnreadMessagesReducer : Reducer<CirclesState, IncrementUnreadMessagesAction>
    {
        public override CirclesState Reduce(CirclesState state, IncrementUnreadMessagesAction action)
        {
            var newState = state.DeepClone();
            if (newState.Circles != null)
            {
                var circle = newState.Circles.SingleFirstOrDefault(c => c.Id == action.CircleId);
                if (circle != null)
                {
                    circle.NumberOfUnreadMessages++;
                }
            }
            return newState;
        }
    }
}
