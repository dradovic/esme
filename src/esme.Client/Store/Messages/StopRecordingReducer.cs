using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class StopRecordingReducer : Reducer<MessagesState, StopRecordingAction>
    {
        public override MessagesState Reduce(MessagesState state, StopRecordingAction action)
        {
            return state.TransitionTo(State.StopRecording);
        }
    }
}
