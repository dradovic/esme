using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostVoiceMessageFailedReducer : Reducer<MessagesState, PostVoiceMessageFailedAction>
    {
        public override MessagesState Reduce(MessagesState state, PostVoiceMessageFailedAction action)
        {
            var newState = state.TransitionTo(State.RecordingAvailable);
            newState.ErrorMessage = action.ErrorMessage;
            return newState;
        }
    }
}
