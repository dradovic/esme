namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesFailedAction
    {
        public FetchInitialMessagesFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}