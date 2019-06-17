using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class FetchInitialMessagesFailedAction : IAction
    {
        public FetchInitialMessagesFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}