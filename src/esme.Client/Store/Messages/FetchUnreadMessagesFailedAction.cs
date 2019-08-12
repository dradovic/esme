namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesFailedAction
    {
        public FetchUnreadMessagesFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}