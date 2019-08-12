using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class StartRecordingReducer : Reducer<MessagesState, StartRecordingAction>
    {
        public override MessagesState Reduce(MessagesState state, StartRecordingAction action)
        {
            return state.TransitionTo(State.IsRecording);
        }
    }
}
