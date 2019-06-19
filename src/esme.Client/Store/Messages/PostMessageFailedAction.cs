using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class PostMessageFailedAction : IAction
    {
        public PostMessageFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}