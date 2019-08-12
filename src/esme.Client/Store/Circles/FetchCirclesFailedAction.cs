namespace esme.Client.Store.Circles
{
    public class FetchCirclesFailedAction
    {
        public FetchCirclesFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}