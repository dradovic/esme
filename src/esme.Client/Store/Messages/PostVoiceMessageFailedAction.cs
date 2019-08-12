namespace esme.Client.Store.Messages
{
    public class PostVoiceMessageFailedAction
    {
        public PostVoiceMessageFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
