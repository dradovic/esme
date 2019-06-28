using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostTextMessageFailedAction : IAction
    {
        public PostTextMessageFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}