using Blazor.Fluxor;

namespace esme.Client.Store.Circles
{
    public class FetchCirclesReducer : Reducer<CirclesState, FetchCirclesAction>
    {
        public override CirclesState Reduce(CirclesState state, FetchCirclesAction action)
        {
            return new CirclesState(
                isLoading: true,
                errorMessage: null,
                circles: null);
        }
    }
}
