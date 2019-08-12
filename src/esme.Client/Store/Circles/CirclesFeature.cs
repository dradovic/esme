using Blazor.Fluxor;

namespace esme.Client.Store.Circles
{
    public class CirclesFeature : Feature<CirclesState>
    {
        public override string GetName() => "Circles";

        protected override CirclesState GetInitialState() => new CirclesState(
            isLoading: false,
            errorMessage: null,
            circles: null);
    }
}
