using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchUnreadMessagesFailedAction : IAction
    {
        public FetchUnreadMessagesFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}