using Blazor.Fluxor;

namespace esme.Client.Store.Messages
{
    public class SubmitMessageFailedAction : IAction
    {
        public SubmitMessageFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}