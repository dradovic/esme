using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostVoiceMessageFailedAction : IAction
    {
        public PostVoiceMessageFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
