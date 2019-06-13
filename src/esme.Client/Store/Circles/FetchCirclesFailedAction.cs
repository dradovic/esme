using Blazor.Fluxor;

namespace esme.Client.Store.Circles
{
    public class FetchCirclesFailedAction : IAction
    {
        public FetchCirclesFailedAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}