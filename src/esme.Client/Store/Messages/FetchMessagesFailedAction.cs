using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchMessagesFailedAction : IAction
    {
        public FetchMessagesFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}