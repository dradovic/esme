using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class AvailRecordingReducer : Reducer<MessagesState, AvailRecordingAction>
    {
        public override MessagesState Reduce(MessagesState state, AvailRecordingAction action)
        {
            var newState = state.TransitionTo(State.RecordingAvailable);
            newState.RecordingUrl = action.RecordingUrl;
            return newState;
        }
    }
}
