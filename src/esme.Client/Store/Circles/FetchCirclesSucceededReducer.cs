using Blazor.Fluxor;

namespace esme.Client.Store.Circles
{
    public class FetchCirclesSucceededReducer : Reducer<CirclesState, FetchCirclesSucceededAction>
    {
        public override CirclesState Reduce(CirclesState state, FetchCirclesSucceededAction action)
        {
            return new CirclesState(
                isLoading: false,
                errorMessage: null,
                circles: action.Circles);
        }
    }
}
