using Blazor.Fluxor;

namespace esme.Client.Store.Circles
{
    public class FetchCirclesFailedReducer : Reducer<CirclesState, FetchCirclesFailedAction>
    {
        public override CirclesState Reduce(CirclesState state, FetchCirclesFailedAction action)
        {
            return new CirclesState(
                isLoading: false,
                errorMessage: action.ErrorMessage,
                circles: null);
        }
    }
}