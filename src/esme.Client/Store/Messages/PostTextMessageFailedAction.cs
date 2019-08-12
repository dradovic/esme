namespace esme.Client.Store.Messages
{
    public class PostTextMessageFailedAction
    {
        public PostTextMessageFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}